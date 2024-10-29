using TestTask1.DTO;
using TestTask1.DTOs;
using TestTask1.Models;
using static TestTask1.Responses.CustomResponses;

namespace TestTask1.Repositories
{
	public interface IApplicationRepo
	{
		Task<List<AddOrderDTO>> GetAllOrdersAsync();
		Task<List<Order>> FilterOrdersAsync(string? cityDistrict, DateTime? firstDeliveryDateTime);
		Task<Order?> GetOrderAsync(int Id);
		Task<BaseResponse> AddOrderAsync(AddOrderDTO model);
		Task<BaseResponse> DeleteOrderAsync(DeleteOrderDTO model);
	}
}
