using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Pizzengineering.App.Data;

internal class Program
{
	public const string ApiName = "Pizzengineering.API";

	private static void Main(string[] args)
	{
		const string ApiUrl = "https://localhost:7026/api/";
		var builder = WebApplication.CreateBuilder(args);

		// Add services to the container.
		builder.Services.AddRazorPages();
		builder.Services.AddServerSideBlazor();
		builder.Services.AddHttpClient(ApiName, e =>
		{
			e.BaseAddress = new Uri(ApiUrl);
		});

		builder.Services.AddScoped<IPizzaService, PizzasService>();

		var app = builder.Build();

		// Configure the HTTP request pipeline.
		if (!app.Environment.IsDevelopment())
		{
			app.UseExceptionHandler("/Error");
			// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
			app.UseHsts();
		}

		app.UseHttpsRedirection();

		app.UseStaticFiles();

		app.UseRouting();

		app.MapBlazorHub();
		app.MapFallbackToPage("/_Host");

		app.Run();
	}
}