using System.Text;
using Newtonsoft.Json;
using Pizzengineering.Application.Pizzas.Queries;
using Pizzengineering.Application.Pizzas.Queries.All;

namespace Pizzengineering.App.Data;

public class PizzasService : IPizzaService
{
	private readonly IHttpClientFactory _client;
	private string[] MediaTypes = new string[] { "application/json" };

	public PizzasService(IHttpClientFactory client)
	{
		_client = client;
	}

	public async Task<List<PizzaResponse>> GetAllPizzasAsync(GetAllPizzasQuery query, CancellationToken cancellationToken = default)
	{
		string endpoint = $"Pizza/All/";
		var json = JsonConvert.SerializeObject(query);
		var client = _client.CreateClient(Program.ApiName);

		var request = new HttpRequestMessage
		{
			Method = HttpMethod.Get,
			RequestUri = new Uri(client.BaseAddress!, endpoint),
			Content = new StringContent(json, Encoding.UTF8, MediaTypes[0])
		};

		HttpResponseMessage response = await client.SendAsync(request, cancellationToken);

		string content = await response.Content.ReadAsStringAsync();

		var pizzas = JsonConvert.DeserializeObject<List<PizzaResponse>>(content);

		return pizzas!;
	}
}
