using ECOO.Application.Interfaces.Repositories;
using ECOO.Application.Interfaces.Services;
using ECOO.Application.Services;
using ECOO.Application.ViewModels;
using ECOO.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testEcoo
{
    public class OrderServiceTests
    {
        private readonly Mock<IOrderRepository> _orderRepo;
        private readonly Mock<ICartService> _cartService;
        private readonly OrderService _service;

        public OrderServiceTests()
        {
            _orderRepo = new Mock<IOrderRepository>();
            _cartService = new Mock<ICartService>();

            _service = new OrderService(_orderRepo.Object, _cartService.Object);
        }

        [Fact]
        public async Task PlaceOrder_ShouldThrow_WhenCartEmpty()
        {
            _cartService.Setup(x => x.GetCart())
                .Returns(new CartViewModel { Items = new List<CartItemViewModel>() });

            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _service.PlaceOrderAsync(new CheckoutViewModel()));
        }

        [Fact]
        public async Task PlaceOrder_ShouldCreateOrder_AndClearCart()
        {
            var cart = new CartViewModel
            {
                Items = new List<CartItemViewModel>
            {
                new() { ProductId = 1, Quantity = 2, Price = 100 }
            }
            };

            _cartService.Setup(x => x.GetCart()).Returns(cart);

            _orderRepo.Setup(x => x.AddAsync(It.IsAny<Order>()))
                .Returns(Task.CompletedTask)
                .Callback<Order>(o => o.Id = 10);

            var result = await _service.PlaceOrderAsync(new CheckoutViewModel
            {
                CustomerName = "Rashad",
                CustomerEmail = "test@test.com",
                ShippingAddress = "Cairo"
            });

            Assert.Equal(10, result);

            _orderRepo.Verify(x => x.AddAsync(It.IsAny<Order>()), Times.Once);
            _cartService.Verify(x => x.ClearCart(), Times.Once);
        }

        [Fact]
        public async Task GetOrderById_ShouldReturnNull_WhenNotFound()
        {
            _orderRepo.Setup(x => x.GetByIdWithItemsAsync(1))
                .ReturnsAsync((Order?)null);

            var result = await _service.GetOrderByIdAsync(1);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllOrders_ShouldReturnList()
        {
            _orderRepo.Setup(x => x.GetAllOrderedByDateAsync())
                .ReturnsAsync(new List<Order>());

            var result = await _service.GetAllOrdersAsync();

            Assert.NotNull(result);
        }
    }
}
