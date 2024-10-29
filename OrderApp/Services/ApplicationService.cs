using TestTask1.DTOs;
using static TestTask1.Responses.CustomResponses;
using TestTask1.DTO;

namespace TestTask1.Services
{
	public class ApplicationService : IApplicationService
	{
		private readonly HttpClient _httpClient;

		public ApplicationService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		private static async Task<T> SendRequestAsync<T>(Func<Task<HttpResponseMessage>> httpRequest)
		{
			var response = await httpRequest();
			if (response.IsSuccessStatusCode)
			{
				return await response.Content.ReadFromJsonAsync<T>() ?? throw new HttpRequestException("Empty response received");
			}
			throw new HttpRequestException($"Request failed with status code {response.StatusCode} and reason {response.ReasonPhrase}.");
		}

		public Task<BaseResponse> GetAllOrdersAsync() =>
			SendRequestAsync<BaseResponse>(() => _httpClient.GetAsync("api/application/getAllOrders"));

		public Task<BaseResponse> FilterOrdersAsync(string? cityDistrict, DateTime? firstDeliveryDateTime) =>
			SendRequestAsync<BaseResponse>(() =>_httpClient.GetAsync($"api/application/filterOrders?cityDistrict={cityDistrict}&firstDeliveryDateTime={firstDeliveryDateTime:yyyy-MM-dd%20HH:mm:ss}"));

		public Task<BaseResponse> DeleteOrderAsync(DeleteOrderDTO model) =>
			SendRequestAsync<BaseResponse>(() => _httpClient.DeleteAsync($"api/application/deleteOrder?id={model.UnicId}"));

		public Task<BaseResponse> AddOrderAsync(AddOrderDTO model) =>
			SendRequestAsync<BaseResponse>(() => _httpClient.PostAsJsonAsync("api/application/addOrder", model));

	}
}
