using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PokemonReviewApp.Controllers;
using PokemonReviewApp.Data.Interfases;
using PokemonReviewApp.Helper;
using PokemonReviewApp.Models;
using PokemonReviewApp.Models.Dto;

namespace PokemonReviewApp.Tests
{
    public class CategoryControllerTests
    {
        private Mock<ICategoryRepository> _categoryRepositoryMock;
        private readonly IMapper _mapper;
        public CategoryControllerTests()
        {
            _categoryRepositoryMock = new Mock<ICategoryRepository>();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfiles());
            });

            _mapper = mappingConfig.CreateMapper();
        }

        [Fact]
        public void GetCategories_ReturnsOkResult()
        {
            // Arrange
            var categories = new List<Category>()
            {
                new Category
                {
                    Id = 1,
                    Name = "category1"
                },
                new Category
                {
                    Id = 2,
                    Name = "category2"
                },
            };

            _categoryRepositoryMock.Setup(repo => repo.GetCategories()).Returns(categories);
            var controller = new CategoryController(_categoryRepositoryMock.Object, _mapper);


            // Act
            var result = controller.GetCategories();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);

            var categoryDtos = Assert.IsType<List<CategoryDto>>(okResult.Value);
            Assert.Equal(2, categoryDtos.Count);

        }

        [Fact]
        public void GetCategoryById_ReturnsOKResult()
        {
            // Arrange
            var categoryId = 1;
            var category = new Category { Id = categoryId, Name = "category1" };

            _categoryRepositoryMock.Setup(repo => repo.CategoriesExists(categoryId)).Returns(true);
            _categoryRepositoryMock.Setup(repo => repo.GetCategory(categoryId)).Returns(category);

            var controller = new CategoryController(_categoryRepositoryMock.Object, _mapper);

            // Act
            var result = controller.GetCategory(categoryId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var categoryDto = Assert.IsType<CategoryDto>(okResult.Value);

            Assert.Equal(1, categoryDto.Id);
            Assert.Equal("category1", categoryDto.Name);
        }

        [Fact]
        public void GetCategoryById_ReturnsNotFoundResult()
        {
            // Arrange
            var categoryId = 1;
            _categoryRepositoryMock.Setup(repo => repo.CategoriesExists(categoryId)).Returns(false);

            var controller = new CategoryController(_categoryRepositoryMock.Object, _mapper);

            //Act
            var result = controller.GetCategory(categoryId);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetPokemonByCategoryId_ReturnsOkResult()
        {
            // Arrange
            var categoryId = 1;
            var pokemons = new List<Pokemon>() {
                new Pokemon { Id = 1, Name = "Pokemon1" } ,
                new Pokemon {Id = 1,Name = "Pokemon2"}
            };

            _categoryRepositoryMock.Setup(repo => repo.GetPokemonByCategory(categoryId)).Returns(pokemons);

            var controller = new CategoryController(_categoryRepositoryMock.Object, _mapper);

            //Act
            var result = controller.GetPokemonByCategoryId(categoryId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var pokemonDtos = Assert.IsType<List<PokemonDto>>( okResult.Value);
            Assert.Equal(2, pokemonDtos.Count);
        }

        [Fact]
        public void CreateCategory_ReturnsOkResult()
        {
            // Arrange
            var categoryDto = new CategoryDto { Id = 1, Name = "category1" };

            var categoryMap = new Category { Id = 1, Name = "category1" };
            _categoryRepositoryMock.Setup(repo => repo.GetCategories()).Returns(new List<Category>());
            _categoryRepositoryMock.Setup(repo => repo.CreateCategory(It.IsAny<Category>())).Returns(true);

            var controller = new CategoryController(_categoryRepositoryMock.Object,_mapper);

            // Act
            var result = controller.CreateCategory(categoryDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
        }

        [Fact] void CreateCategory_ReturnsBadRequestResult()
        {
            // Arrange
            CategoryDto categoryDto = null;
            
            var controller = new CategoryController(_categoryRepositoryMock.Object,_mapper);

            // Act
            var result = controller.CreateCategory(categoryDto);

            // Assert
            var badResult = Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
