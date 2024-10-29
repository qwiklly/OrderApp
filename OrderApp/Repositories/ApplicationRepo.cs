using static TestTask1.Responses.CustomResponses;
using TestTask1.Data;
using TestTask1.DTO;
using TestTask1.Models;
using Microsoft.EntityFrameworkCore;
using TestTask1.DTOs;

namespace TestTask1.Repositories
{
	public class ApplicationRepo : IApplicationRepo
	{
		private readonly AppDbContext _appDbContext;
		private readonly ILogger<ApplicationRepo> _logger;

		public ApplicationRepo(AppDbContext appDbContext, ILogger<ApplicationRepo> logger)
		{
			_appDbContext = appDbContext;
			_logger = logger;
		}

		public async Task<BaseResponse> AddOrderAsync(AddOrderDTO model)
		{
			try
			{
				var findOrder = await _appDbContext.Orders.FirstOrDefaultAsync(x => x.UnicId == model.UnicId);
				if (findOrder != null)
					return new BaseResponse(false, "Order already exists");

				_appDbContext.Orders.Add(new Order
				{
					Id = model.Id,
					UnicId = model.UnicId,
					Weight = model.Weight,
					Location = model.Location,
					DateTime = model.DateTime,
				});

				await _appDbContext.SaveChangesAsync();
				return new BaseResponse(true, "Order added successfully");
			}
			catch(Exception ex) 
			{
				_logger.LogError(ex, "Error while adding order");
				return new BaseResponse(false, "Error while adding order");
			}
		}

		public async Task<List<Order>> FilterOrdersAsync(string? cityDistrict, DateTime? firstDeliveryDateTime)
		{
			try
			{
				var query = _appDbContext.Orders.AsQueryable();

				if (!string.IsNullOrWhiteSpace(cityDistrict))
				{
					query = query.Where(order => order.Location.Contains(cityDistrict));
				}

				if (firstDeliveryDateTime.HasValue)
				{
					query = query.Where(order => order.DateTime >= firstDeliveryDateTime.Value
												 && order.DateTime <= firstDeliveryDateTime.Value.AddMinutes(30));
				}

				return await query.ToListAsync();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Ошибка во время фильтрации заказа");
				return new List<Order>(); 
			}
		}

		public async Task<BaseResponse> DeleteOrderAsync(DeleteOrderDTO model)
		{
			try
			{
				var findOrder = await GetOrderAsync(model.UnicId);
				if (findOrder != null)
				{
					_appDbContext.Orders.Remove(findOrder);
					await _appDbContext.SaveChangesAsync();
					return new BaseResponse(true, "Заказ успешно удален");
				}
				return new BaseResponse(false, "Заказ не найден");
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Ошибка во время удаления заказа");
				return new BaseResponse(false, "Ошибка во время удаления заказа");
			}
		}

		public async Task<List<AddOrderDTO>> GetAllOrdersAsync()
		{
			try
			{
				return await _appDbContext.Orders
					.Select(x => new AddOrderDTO
					{
						UnicId = x.UnicId,
						Weight = x.Weight,
						Location = x.Location,
						DateTime = x.DateTime
					})
					.ToListAsync();
			}
			catch(Exception ex) 
			{
				_logger.LogError(ex, "Ошибка во время получения всех заказов");
				throw new InvalidOperationException("Ошибка во время получения всех заказов");
			}
		}

		public async Task<Order?> GetOrderAsync(int unicId)
			=> await _appDbContext.Orders.FirstOrDefaultAsync(e => e.UnicId == unicId);

	}
}
