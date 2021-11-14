using AutoMapper;
using Moq;
using ShopBridge.BusinessLogic;
using ShopBridge.DataTransferObjects;
using ShopBridge.Models;
using ShopBridge.Repository;
using ShopBridge.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ShopBridge.Test.TestBusinessLogic
{
    public class InventoryBusinessLogicTest
    {
        private readonly Mock<IGenericRepository<Product>> productGenericRepository;
        private readonly InventoryBusinessLogic inventoryBusinessLogic;

        public InventoryBusinessLogicTest()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ShopBridgeAutoMapper());
            });
            var mapper = mockMapper.CreateMapper();
            productGenericRepository = new Mock<IGenericRepository<Product>>();
            inventoryBusinessLogic = new InventoryBusinessLogic(productGenericRepository.Object, mapper);
        }

        [Fact]
        public void AddNewProduct_NoSuchSupplierResult()
        {
            // Arrange
            var addProductRequestDataTransferObject = new AddProductRequestDataTransferObject
            {
                ProductName = "NewProduct",
                ProductPrice = 10.9,
                ProductQuantity = 30,
                ProductUnit = "KG",
                CategoryId = 1,
                SupplierId = 10
            };
            var supplierBusinessLogic = new Mock<ISupplierBussinessLogic>();
            var categoryBusinessLogic = new Mock<ICategoryBusinessLogic>();
            supplierBusinessLogic.Setup(x => x.GetSupplier(It.IsAny<int>())).ReturnsAsync(() => null);

            // Act
            var result = inventoryBusinessLogic.AddNewProduct(addProductRequestDataTransferObject,supplierBusinessLogic.Object,
                categoryBusinessLogic.Object).Result;

            // Assert
            Assert.Equal(HttpStatusCode.Forbidden, result.StatusCode);
            Assert.Equal("No such supplier available", result.Message);
        }


        [Fact]
        public void AddNewProduct_NoSuchProductCategoryResult()
        {
            // Arrange
            var addProductRequestDataTransferObject = new AddProductRequestDataTransferObject
            {
                ProductName = "NewProduct",
                ProductPrice = 10.9,
                ProductQuantity = 30,
                ProductUnit = "KG",
                CategoryId = 10,
                SupplierId = 1
            };
            var supplierBusinessLogic = new Mock<ISupplierBussinessLogic>();
            var categoryBusinessLogic = new Mock<ICategoryBusinessLogic>();
            supplierBusinessLogic.Setup(x => x.GetSupplier(It.IsAny<int>())).ReturnsAsync(() => new Supplier());
            categoryBusinessLogic.Setup(x => x.GetProductCategory(It.IsAny<int>())).ReturnsAsync(() => null);

            // Act
            var result = inventoryBusinessLogic.AddNewProduct(addProductRequestDataTransferObject, supplierBusinessLogic.Object,
                categoryBusinessLogic.Object).Result;

            // Assert
            Assert.Equal(HttpStatusCode.Forbidden, result.StatusCode);
            Assert.Equal("No such product category available", result.Message);
        }

        [Fact]
        public void AddNewProduct_ProductCurentlyAvailableResult()
        {
            // Arrange
            var addProductRequestDataTransferObject = new AddProductRequestDataTransferObject
            {
                ProductName = "NewProduct",
                ProductPrice = 10.9,
                ProductQuantity = 30,
                ProductUnit = "KG",
                CategoryId = 10,
                SupplierId = 1
            };
            var supplierBusinessLogic = new Mock<ISupplierBussinessLogic>();
            var categoryBusinessLogic = new Mock<ICategoryBusinessLogic>();
            supplierBusinessLogic.Setup(x => x.GetSupplier(It.IsAny<int>())).ReturnsAsync(() => new Supplier());
            categoryBusinessLogic.Setup(x => x.GetProductCategory(It.IsAny<int>())).ReturnsAsync(() => new Category());
            productGenericRepository.Setup(x=>x.FirstOrDefault(It.IsAny<Expression<Func<Product,bool>>>())).ReturnsAsync(() => new Product());

            // Act
            var result = inventoryBusinessLogic.AddNewProduct(addProductRequestDataTransferObject, supplierBusinessLogic.Object,
                categoryBusinessLogic.Object).Result;

            // Assert
            Assert.Equal(HttpStatusCode.Forbidden, result.StatusCode);
            Assert.Equal("Product currently available", result.Message);
        }

        [Fact]
        public void AddNewProduct_ProductAddedSucessfullyResult()
        {
            // Arrange
            var addProductRequestDataTransferObject = new AddProductRequestDataTransferObject
            {
                ProductName = "NewProduct",
                ProductPrice = 10.9,
                ProductQuantity = 30,
                ProductUnit = "KG",
                CategoryId = 1,
                SupplierId = 1
            };
            var supplierBusinessLogic = new Mock<ISupplierBussinessLogic>();
            var categoryBusinessLogic = new Mock<ICategoryBusinessLogic>();
            supplierBusinessLogic.Setup(x => x.GetSupplier(It.IsAny<int>())).ReturnsAsync(() => new Supplier());
            categoryBusinessLogic.Setup(x => x.GetProductCategory(It.IsAny<int>())).ReturnsAsync(() => new Category());
            productGenericRepository.Setup(x => x.FirstOrDefault(It.IsAny<Expression<Func<Product, bool>>>())).ReturnsAsync(() => null);

            // Act
            var result = inventoryBusinessLogic.AddNewProduct(addProductRequestDataTransferObject, supplierBusinessLogic.Object,
                categoryBusinessLogic.Object).Result;

            // Assert
            Assert.Equal(HttpStatusCode.Created, result.StatusCode);
            Assert.Equal("Product Added Successfully", result.Message);
        }

        [Fact]
        public void DeleteProduct_NoSuchProductAvailableResult()
        {
            // Arrange
            int productId = 1;
            
            productGenericRepository.Setup(x => x.FirstOrDefault(It.IsAny<Expression<Func<Product, bool>>>())).ReturnsAsync(() => null);

            // Act
            var result = inventoryBusinessLogic.DeleteProduct(productId).Result;

            // Assert
            Assert.Equal(HttpStatusCode.Forbidden, result.StatusCode);
            Assert.Equal("No such product available", result.Message);
        }

        [Fact]
        public void DeleteProduct_ProductRemoveSucessfullyResult()
        {
            // Arrange
            int productId = 1;

            productGenericRepository.Setup(x => x.FirstOrDefault(It.IsAny<Expression<Func<Product, bool>>>())).ReturnsAsync(() => new Product());

            // Act
            var result = inventoryBusinessLogic.DeleteProduct(productId).Result;

            // Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.Equal("Product Removed Successfully", result.Message);
        }

        [Fact]
        public void GetAllProduct_NoContentResult()
        {
            // Arrange
            var pagedParameters = new PagedParameters();
            productGenericRepository.Setup(x => x.GetAll()).ReturnsAsync(() => null);

            // Act
            var result = inventoryBusinessLogic.GetAllProducts(pagedParameters).Result;

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);
            Assert.Null(result.Data);
        }

        [Fact]
        public void GetAllProduct_PagedResult()
        {
            // Arrange
            var pagedParameters = new PagedParameters();
            var productsPagedData = new PagedModel<Product>
            {
                CurrentPage = 1,
                PageSize = 10,
                TotalItems = 2,
                Items = new List<Product>
                {
                    new Product
                    {
                       Id =1,
                       ProductName = "NewProduct",
                       ProductPrice = 10.9,
                       ProductQuantity = 30,
                       ProductUnit = "KG",
                       CategoryId = 1,
                       SupplierId = 1,
                       ProductDescription ="Food Product",
                       ProductStatus = "Fresh"
                    },
                    new Product
                    {
                       Id =2,
                       ProductName = "NewProduct1",
                       ProductPrice = 19.9,
                       ProductQuantity = 10,
                       ProductUnit = "KG",
                       CategoryId = 2,
                       SupplierId = 1,
                       ProductDescription ="Wooden Product",
                       ProductStatus = "New"
                    }
                }
            };
            productGenericRepository.Setup(x => x.GetAllPagedresultWithIncludes(
                It.IsAny<PagedParameters>(),It.IsAny<Func<Product,object>>(),It.IsAny<IEnumerable<Expression<Func<Product,object>>>>()))
                .ReturnsAsync(() => productsPagedData);

            // Act
            var result = inventoryBusinessLogic.GetAllProducts(pagedParameters).Result;

            // Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.NotNull(result.Data);
        }

        [Fact]
        public void ModifyProduct_NoSuchProductAvailableResult()
        {
            // Arrange
            var modifyProductRequestDataTransferObject = new ModifyProductRequestDataTransferObject
            {
                ProductId = 1,
                ProductName = "Product1",
                ProductPrice = 12.8,
                ProductQuantity = 6,
                ProductUnit = "KG"
            };

            productGenericRepository.Setup(x => x.FirstOrDefault(It.IsAny<Expression<Func<Product, bool>>>())).ReturnsAsync(() => null);

            // Act
            var result = inventoryBusinessLogic.ModifyProduct(modifyProductRequestDataTransferObject).Result;

            // Assert
            Assert.Equal(HttpStatusCode.Forbidden, result.StatusCode);
            Assert.Equal("No such product available", result.Message);
        }

        [Fact]
        public void ModifyProduct_ProductUpdatedSucessfullyResult()
        {
            // Arrange
            var modifyProductRequestDataTransferObject = new ModifyProductRequestDataTransferObject
            {
                ProductId = 1,
                ProductName = "Product1",
                ProductPrice = 12.8,
                ProductQuantity = 6,
                ProductUnit = "KG"
            };

            productGenericRepository.Setup(x => x.FirstOrDefault(It.IsAny<Expression<Func<Product, bool>>>())).ReturnsAsync(() => new Product());

            // Act
            var result = inventoryBusinessLogic.ModifyProduct(modifyProductRequestDataTransferObject).Result;

            // Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.Equal("Product Updated Successfully", result.Message);
        }
    }
}
