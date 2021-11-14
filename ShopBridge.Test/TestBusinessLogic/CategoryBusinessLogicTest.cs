using Moq;
using ShopBridge.BusinessLogic;
using ShopBridge.Models;
using ShopBridge.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ShopBridge.Test.TestBusinessLogic
{
    public class CategoryBusinessLogicTest
    {
        private readonly Mock<IGenericRepository<Category>> categoryGenericRepository;
        private readonly CategoryBusinessLogic categoryBusinessLogic;

        public CategoryBusinessLogicTest()
        {
            categoryGenericRepository = new Mock<IGenericRepository<Category>>();
            categoryBusinessLogic = new CategoryBusinessLogic(categoryGenericRepository.Object);
        }

        [Fact]
        public void GetCategory_CorrectResult()
        {
            // Arrange
            var categoryId = 1;
            var category = new Category
            {
                Id = 1,
                CategoryName = "Food",
                Description = "Food Products"
            };
            categoryGenericRepository.Setup(x => x.FirstOrDefault(It.IsAny<Expression<Func<Category, bool>>>()))
                .ReturnsAsync(() => category);


            // Act
            var result = categoryBusinessLogic.GetProductCategory(categoryId).Result;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Food", result.CategoryName);
        }
    }
}
