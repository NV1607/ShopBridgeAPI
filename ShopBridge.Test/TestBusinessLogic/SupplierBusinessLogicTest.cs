using Moq;
using ShopBridge.BusinessLogic;
using ShopBridge.Models;
using ShopBridge.Repository;
using System;
using System.Linq.Expressions;
using Xunit;

namespace ShopBridge.Test.TestBusinessLogic
{
    public class SupplierBusinessLogicTest
    {
        private readonly Mock<IGenericRepository<Supplier>> supplierGenericRepository;
        private readonly SupplierBussinessLogic supplierBusinessLogic;

        public SupplierBusinessLogicTest()
        {
            supplierGenericRepository = new Mock<IGenericRepository<Supplier>>();
            supplierBusinessLogic = new SupplierBussinessLogic(supplierGenericRepository.Object);
        }

        [Fact]
        public void GetSupplier_CorrectResult()
        {
            // Arrange
            var supplierId = 1;
            var supplier = new Supplier
            {
                Id = 1,
                Name = "RahulEnterprice",
                Email = "rahulent@gmail.com",
                Address = "BTM 2nd Stage",
                PhoneNumber = 9678653477
            };
            supplierGenericRepository.Setup(x => x.FirstOrDefault(It.IsAny<Expression<Func<Supplier, bool>>>()))
                .ReturnsAsync(() => supplier);


            // Act
            var result = supplierBusinessLogic.GetSupplier(supplierId).Result;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("RahulEnterprice",result.Name);
        }
    }
}
