using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Pizzengineering.API.Configurations;
using Pizzengineering.Application.Behaviors;
using Pizzengineering.Domain.Abstractions;
using Pizzengineering.Infrastructure.Authentication;
using Pizzengineering.Infrastructure.Authentication.Setup;
using Pizzengineering.Infrastructure.BackgroundJobs;
using Pizzengineering.Persistence;
using Pizzengineering.Persistence.Interceptors;
using Pizzengineering.Persistence.Repositories;
using Quartz;
using Scrutor;

internal class Program
{
	private static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		builder.Services.AddControllers();
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen();

		builder.Services.AddTransient<IUserRepository, UserRepository>();
		builder.Services.AddTransient<IPizzaRepository, PizzaRepository>();
		builder.Services.AddTransient<IOrderRepository, OrderRepository>();
		builder.Services.AddTransient<IPaymentInfoRepository, PaymentInfoRepository>();
		builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

		builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

		builder
		.Services
		.Scan(
			selector => selector
				.FromAssemblies(
					Pizzengineering.Infrastructure.AssemblyReference.Assembly,
					Pizzengineering.Persistence.AssemblyReference.Assembly)
				.AddClasses(false)
				.UsingRegistrationStrategy(RegistrationStrategy.Skip)
				.AsMatchingInterface()
				.WithScopedLifetime());

		builder.Services.AddMediatR(configuration =>
			configuration.RegisterServicesFromAssembly(Pizzengineering.Application.AssemblyReference.Assembly));

		builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));

		builder.Services.AddValidatorsFromAssembly(
			Pizzengineering.Application.AssemblyReference.Assembly,
			includeInternalTypes: true);

		string connectionString = builder.Configuration.GetConnectionString("Database")!;

		builder.Services.AddSingleton<ConvertDomainEventsToOutboxMessagesInterceptor>();

		builder.Services.AddDbContext<ApplicationDbContext>(
		(sp, optionsBuilder) =>
		{
			optionsBuilder.UseSqlServer(connectionString);
		});

		builder.Services.AddScoped<IJob, ProcessOutboxMessagesJob>();

		builder.Services.AddQuartz(configure =>
		{
			var jobKey = new JobKey(nameof(ProcessOutboxMessagesJob));

			configure
				.AddJob<ProcessOutboxMessagesJob>(jobKey)
				.AddTrigger(
					trigger =>
						trigger.ForJob(jobKey)
							.WithSimpleSchedule(
								schedule =>
									schedule.WithIntervalInSeconds(100)
										.RepeatForever()));

			configure.UseMicrosoftDependencyInjectionJobFactory();
		});

		builder.Services.AddQuartzHostedService();

		builder
		.Services
		.AddControllers()
		.AddApplicationPart(Pizzengineering.Presentation.AssemblyReference.Assembly)
		.AddNewtonsoftJson();

		builder.Services.AddSwaggerConfiguration();

		builder.Services.ConfigureOptions<JwtOptionsSetup>();
		builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();

		builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			.AddJwtBearer();

		builder.Services.AddAuthorization();
		builder.Services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
		builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();

		var app = builder.Build();

		if (app.Environment.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI();
		}

		app.UseHttpsRedirection();

		app.UseAuthentication();

		app.UseAuthorization();

		app.MapControllers();

		app.Run();
	}
}