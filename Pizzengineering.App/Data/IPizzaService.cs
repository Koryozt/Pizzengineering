using Pizzengineering.Application.Pizzas.Queries;
using Pizzengineering.Application.Pizzas.Queries.All;

namespace Pizzengineering.App.Data;

public interface IPizzaService
{
	Task<List<PizzaResponse>> GetAllPizzasAsync(
		GetAllPizzasQuery query, 
		CancellationToken cancellationToken = default);
}
