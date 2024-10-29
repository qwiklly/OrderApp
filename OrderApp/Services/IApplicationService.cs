using TestTask1.DTO;
using TestTask1.DTOs;
using static TestTask1.Responses.CustomResponses;

namespace TestTask1.Services
{
	public interface IApplicationService
	{
		Task<BaseResponse> GetAllOrdersAsync();
		Task<BaseResponse> FilterOrdersAsync(string? cityDistrict, DateTime? firstDeliveryDateTime);
		Task<BaseResponse> AddOrderAsync(AddOrderDTO model);
		Task<BaseResponse> DeleteOrderAsync(DeleteOrderDTO model);
	}
}
