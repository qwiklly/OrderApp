using Microsoft.AspNetCore.Mvc;
using Moq;
using TestTask1.Controllers;
using TestTask1.DTO;
using TestTask1.DTOs;
using TestTask1.Models;
using TestTask1.Repositories;
using static TestTask1.Responses.CustomResponses;

namespace UnitTests
{
	public class ApplicationRepoTests
	{
		[Fact]
		public async Task AddOrderAsync_ShouldReturnSuccessResponse_WhenOrderIsNew()
		{
			// Arrange
			var mockRepo = new Mock<IApplicationRepo>();
			var orderDto = new AddOrderDTO
			{
				Id = 1,
				UnicId = 123,
				Weight = 10.5,
				Location = "Test Location",
				DateTime = DateTime.Now
			};

			var expectedResponse = new BaseResponse(true, "Order added successfully");
			mockRepo.Setup(repo => repo.AddOrderAsync(orderDto)).ReturnsAsync(expectedResponse);

			var controller = new ApplicationController(mockRepo.Object);

			// Act
			var result = await controller.AddOrderAsync(orderDto);

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result.Result);
			var actualResponse = Assert.IsType<BaseResponse>(okResult.Value);
			Assert.True(actualResponse.Flag == true);
			Assert.Equal("Order added successfully", actualResponse.Message);
		}


		[Fact]
		public async Task GetOrdersAsync_ShouldReturnListOfOrders()
		{
			// Arrange
			var mockRepo = new Mock<IApplicationRepo>();
			var mockOrders = new List<AddOrderDTO>
			{
				new AddOrderDTO { Id = 1, UnicId = 123, Location = "Test Location 1", DateTime = DateTime.Now, Weight = 10.0 },
				new AddOrderDTO { Id = 2, UnicId = 456, Location = "Test Location 2", DateTime = DateTime.Now, Weight = 20.0 }
			};

			mockRepo.Setup(repo => repo.GetAllOrdersAsync()).ReturnsAsync(mockOrders);

			var controller = new ApplicationController(mockRepo.Object);

			// Act
			var result = await controller.GetAllOrdersAsync();

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result.Result);
			var returnValue = Assert.IsType<List<AddOrderDTO>>(okResult.Value);
			Assert.Equal(2, returnValue.Count);
		}

		[Fact]
		public async Task DeleteOrderAsync_ShouldReturnSuccessResponse_WhenOrderExists()
		{
			// Arrange
			var mockRepo = new Mock<IApplicationRepo>();
			var deleteOrderDto = new DeleteOrderDTO { UnicId = 123 };

			var existingOrder = new Order { Id = 1, UnicId = 123, Location = "Test Location", DateTime = DateTime.Now };

			mockRepo.Setup(repo => repo.GetOrderAsync(deleteOrderDto.UnicId)).ReturnsAsync(existingOrder);
			mockRepo.Setup(repo => repo.DeleteOrderAsync(deleteOrderDto)).ReturnsAsync(new BaseResponse(true, "Заказ успешно удален"));

			var controller = new ApplicationController(mockRepo.Object);

			// Act
			var result = await controller.DeleteOrderAsync(deleteOrderDto);

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result.Result);
			var actualResponse = Assert.IsType<BaseResponse>(okResult.Value);
			Assert.True(actualResponse.Flag);
			Assert.Equal("Заказ успешно удален", actualResponse.Message);
		}

		[Fact]
		public async Task FilterOrdersAsync_ShouldReturnFilteredOrders_WhenCriteriaMatch()
		{
			// Arrange
			var mockRepo = new Mock<IApplicationRepo>();
			string cityDistrict = "Test Location";
			DateTime? firstDeliveryDateTime = DateTime.Now;

			var filteredOrders = new List<Order>
			{
				new Order { Id = 1, UnicId = 123, Location = "Test Location", DateTime = firstDeliveryDateTime.Value.AddMinutes(10) }
			};

			mockRepo.Setup(repo => repo.FilterOrdersAsync(cityDistrict, firstDeliveryDateTime)).ReturnsAsync(filteredOrders);

			var controller = new ApplicationController(mockRepo.Object);

			// Act
			var result = await controller.FilterOrdersAsync(cityDistrict, firstDeliveryDateTime);

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result.Result);
			var returnValue = Assert.IsType<List<Order>>(okResult.Value);
			Assert.Single(returnValue);
			Assert.Equal("Test Location", returnValue[0].Location);
			Assert.InRange(returnValue[0].DateTime, firstDeliveryDateTime.Value, firstDeliveryDateTime.Value.AddMinutes(30));
		}

	}
}

