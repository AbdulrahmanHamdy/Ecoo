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
    public class CategoryServiceTests
    {
        private readonly Mock<ICategoryRepository> _repoMock;
        private readonly CategoryService _service;

        public CategoryServiceTests()
        {
            _repoMock = new Mock<ICategoryRepository>();
            _service = new CategoryService(_repoMock.Object);
        }

        [Fact]
        public async Task GetAllCategoriesAsync_ShouldReturnMappedData()
        {
            _repoMock.Setup(x => x.GetAllAsync())
                .ReturnsAsync(new List<Category>
                {
                new() { Id = 1, Name = "Cat1" }
                });

            var result = await _service.GetAllCategoriesAsync();

            Assert.Single(result);
            Assert.Equal("Cat1", result.First().Name);
        }

        [Fact]
        public async Task GetCategoryById_ShouldReturnNull_WhenNotFound()
        {
            _repoMock.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Category?)null);

            var result = await _service.GetCategoryByIdAsync(1);

            Assert.Null(result);
        }

        [Fact]
        public async Task CreateCategory_ShouldCallRepo()
        {
            var vm = new CategoryViewModel { Id = 1, Name = "Test" };

            await _service.CreateCategoryAsync(vm);

            _repoMock.Verify(x => x.AddAsync(It.IsAny<Category>()), Times.Once);
        }

        [Fact]
        public async Task DeleteCategory_ShouldCallRepo()
        {
            await _service.DeleteCategoryAsync(5);

            _repoMock.Verify(x => x.DeleteAsync(5), Times.Once);
        }
    }
}

