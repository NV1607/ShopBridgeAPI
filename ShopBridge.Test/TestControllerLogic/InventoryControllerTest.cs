using Microsoft.AspNetCore.Mvc;
using Moq;
using ShopBridge.BusinessLogic;
using ShopBridge.Controllers;
using ShopBridge.DataTransferObjects;
using ShopBridge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ShopBridge.Test.TestControllerLogic
{
    public class InventoryControllerTest
    {
        private readonly Mock<IInventoryBusinessLogic> inventoryBusinessLogic;
        private readonly InventoryController inventoryController;

        public InventoryControllerTest()
        {
            inventoryBusinessLogic = new Mock<IInventoryBusinessLogic>();
            inventoryController = new InventoryController(inventoryBusinessLogic.Object);
        }

        [Fact]
        public void GetAllProduct_PagedResult()
        {
            // Arrange
            var pagedParameters = new PagedParameters();
            var productsPagedData = new PagedModel<ProductResponseDataTransferObject>
            {
                CurrentPage = 1,
                PageSize = 10,
                TotalItems = 2,
                Items = new List<ProductResponseDataTransferObject>
                {
                    new ProductResponseDataTransferObject
                    {
                       Id =1,
                       ProductName = "NewProduct",
                       ProductPrice = 10.9,
                       ProductQuantity = 30,
                       ProductUnit = "KG",
                       CategoryId = 1,
                       CategoryName = "Food",
                       SupplierId = 1,
                       SupplierName ="RahulEnterprice",
                       ProductDescription ="Food Product",
                       ProductStatus = "Fresh"
                    },
                    new ProductResponseDataTransferObject
                    {
                       Id =2,
                       ProductName = "NewProduct1",
                       ProductPrice = 19.9,
                       ProductQuantity = 10,
                       ProductUnit = "KG",
                       CategoryId = 2,
                       CategoryName = "Wooden",
                       SupplierId = 1,
                       SupplierName ="RahulEnterprice",
                       ProductDescription ="Wooden Product",
                       ProductStatus = "New"
                    }
                }
            };
            inventoryBusinessLogic.Setup(x => x.GetAllProducts(
                It.IsAny<PagedParameters>())).ReturnsAsync(() => new BaseResponse
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Data = productsPagedData
                });

            // Act
            var result =(ObjectResult) inventoryController.GetAllProducts(pagedParameters).Result;

            // Assert
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
            Assert.NotNull(result.Value);
        }

        [Fact]
        public void AddNewProduct_BadRequestResult()
        {
            // Arrange
            var addProductRequestDataTransferObject = new AddProductRequestDataTransferObject();
            var supplierBusinessLogic = new Mock<ISupplierBussinessLogic>();
            var categoryBusinessLogic = new Mock<ICategoryBusinessLogic>();

            // Act
            var result = (ObjectResult)inventoryController.AddNewProduct(addProductRequestDataTransferObject, supplierBusinessLogic.Object,
                categoryBusinessLogic.Object).Result;

            // Assert
            Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);
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
            inventoryBusinessLogic.Setup(x => x.AddNewProduct(
                It.IsAny<AddProductRequestDataTransferObject>(),supplierBusinessLogic.Object,categoryBusinessLogic.Object)).ReturnsAsync(() => new BaseResponse
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Message = "Product Added Successfully"
                });

            // Act
            var result = (ObjectResult)inventoryController.AddNewProduct(addProductRequestDataTransferObject, supplierBusinessLogic.Object,
                categoryBusinessLogic.Object).Result;

            // Assert
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
            Assert.NotNull(result.Value);
        }

        [Fact]
        public void UpdateProduct_BadRequestResult()
        {
            // Arrange
            var modifyProductRequestDataTransferObject = new ModifyProductRequestDataTransferObject();

            // Act
            var result = (ObjectResult)inventoryController.UpdateProduct(modifyProductRequestDataTransferObject).Result;

            // Assert
            Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public void UpdateProduct_ProductUpdatedSucessfullyResult()
        {
            // Arrange
            var modifyProductRequestDataTransferObject = new ModifyProductRequestDataTransferObject
            {
                ProductId = 1,
                ProductName = "NewProduct",
                ProductPrice = 10.9,
                ProductQuantity = 30,
                ProductUnit = "KG",
            };
           
            inventoryBusinessLogic.Setup(x => x.ModifyProduct(
                It.IsAny<ModifyProductRequestDataTransferObject>())).ReturnsAsync(() => new BaseResponse
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Message = "Product Updated Successfully"
                });

            // Act
            var result = (ObjectResult)inventoryController.UpdateProduct(modifyProductRequestDataTransferObject).Result;

            // Assert
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
            Assert.NotNull(result.Value);
        }

        [Fact]
        public void DeleteProduct_BadRequestResult()
        {
            // Arrange
            var productId = 0;

            // Act
            var result = (ObjectResult)inventoryController.RemoveProduct(productId).Result;

            // Assert
            Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public void DeleteProduct_ProductRemovedSucessfullyResult()
        {
            // Arrange
            var productId = 1;

            inventoryBusinessLogic.Setup(x => x.DeleteProduct(
                It.IsAny<int>())).ReturnsAsync(() => new BaseResponse
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Message = "Product Removed Successfully"
                });

            // Act
            var result = (ObjectResult)inventoryController.RemoveProduct(productId).Result;

            // Assert
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
            Assert.NotNull(result.Value);
        }
    }
}
