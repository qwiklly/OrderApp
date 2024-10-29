using Microsoft.AspNetCore.Mvc;
using TestTask1.DTO;
using TestTask1.DTOs;
using TestTask1.Repositories;
using static TestTask1.Responses.CustomResponses;

namespace TestTask1.Controllers
{
	[Route("api/application")]
	[ApiController]
	public class ApplicationController : ControllerBase
	{
		private readonly IApplicationRepo _applicationRepo;

		public ApplicationController(IApplicationRepo applicationRepo)
		{
			_applicationRepo = applicationRepo;
		}

		/// <summary>
		/// Фильтрует заказ по местоположению и (или) времени заказа.
		/// </summary>
		/// <returns>Получение заказа или ошибка</returns>
		[HttpGet("filterOrders")]
		public async Task<ActionResult<BaseResponse>> FilterOrdersAsync(
		[FromQuery] string? cityDistrict = null,
		[FromQuery] DateTime? firstDeliveryDateTime = null)
		{
			var result = await _applicationRepo.FilterOrdersAsync(cityDistrict, firstDeliveryDateTime);
			return Ok(result);
		}

		/// <summary>
		/// Получает список всех заказов.
		/// </summary>
		/// <returns>Список заказов или ошибка</returns>
		[HttpGet("getAllOrders")]
		public async Task<ActionResult<BaseResponse>> GetAllOrdersAsync()
		{
			var result = await _applicationRepo.GetAllOrdersAsync();
			return Ok(result);
		}

		/// <summary>
		/// Удаляет заказ.
		/// </summary>
		/// <param name="model">Модель данных для удаления заказа.</param>
		/// <returns>Результат удаления заказа или ошибка</returns>
		[HttpDelete("deleteOrder")]
		public async Task<ActionResult<BaseResponse>> DeleteOrderAsync([FromBody] DeleteOrderDTO model)
		{
			var result = await _applicationRepo.DeleteOrderAsync(model);
			return Ok(result);
		}
		/// <summary>
		/// Добавляем заказ
		/// </summary>
		/// <returns>Заказ или ошибка</returns>
		[HttpPost("addOrder")]
		public async Task<ActionResult<BaseResponse>> AddOrderAsync(AddOrderDTO model)
		{
			var result = await _applicationRepo.AddOrderAsync(model);
			return Ok(result);
		}
	}
}
