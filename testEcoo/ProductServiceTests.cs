using ECOO.Application.Interfaces.Repositories;
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
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _repoMock;
        private readonly ProductService _service;

        public ProductServiceTests()
        {
            _repoMock = new Mock<IProductRepository>();
            _service = new ProductService(_repoMock.Object);
        }

        [Fact]
        public async Task GetAllProducts_ShouldReturnMappedData()
        {
            _repoMock.Setup(x => x.GetAllAsync())
                .ReturnsAsync(new List<Product>
                {
                new() { Id = 1, Name = "P1", Price = 100 }
                });

            var result = await _service.GetAllProductsAsync();

            Assert.Single(result);
            Assert.Equal("P1", result.First().Name);
        }

        [Fact]
        public async Task SearchProducts_ShouldReturnAll_WhenQueryEmpty()
        {
            _repoMock.Setup(x => x.GetAllWithCategoryAsync())
                .ReturnsAsync(new List<Product>());

            var result = await _service.SearchProductsAsync("");

            Assert.NotNull(result);
        }

        [Fact]
        public async Task CreateProduct_ShouldCallRepo()
        {
            var vm = new ProductViewModel
            {
                Name = "Test",
                Price = 10
            };

            await _service.CreateProductAsync(vm);

            _repoMock.Verify(x => x.AddAsync(It.IsAny<Product>()), Times.Once);
        }

        [Fact]
        public async Task DeleteProduct_ShouldCallRepo()
        {
            await _service.DeleteProductAsync(1);

            _repoMock.Verify(x => x.DeleteAsync(1), Times.Once);
        }
    }
}

