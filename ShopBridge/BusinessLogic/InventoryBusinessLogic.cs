using AutoMapper;
using ShopBridge.Database;
using ShopBridge.DataTransferObjects;
using ShopBridge.Models;
using ShopBridge.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;

namespace ShopBridge.BusinessLogic
{
    /// <summary>
    /// InventoryBusinessLogic
    /// </summary>
    public class InventoryBusinessLogic : IInventoryBusinessLogic
    {
        private readonly IGenericRepository<Product> _productGenericRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Inventory BusinessLogic Constructor
        /// </summary>
        public InventoryBusinessLogic(IGenericRepository<Product> productGenericRepository,
            IMapper mapper)
        {
            _productGenericRepository = productGenericRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Add New Product
        /// </summary>
        /// <param name="addProductRequestDataTransferObject"></param>
        /// <param name="supplierBussinessLogic"></param>
        /// <param name="categoryBusinessLogic"></param>
        /// <returns></returns>
        public async Task<BaseResponse> AddNewProduct(AddProductRequestDataTransferObject addProductRequestDataTransferObject,
            ISupplierBussinessLogic supplierBussinessLogic,ICategoryBusinessLogic categoryBusinessLogic)
        {
            if(null == await supplierBussinessLogic.GetSupplier(addProductRequestDataTransferObject.SupplierId))
                return new BaseResponse { StatusCode = HttpStatusCode.Forbidden, Message = "No such supplier available" };

            if (null == await categoryBusinessLogic.GetProductCategory(addProductRequestDataTransferObject.CategoryId))
                return new BaseResponse { StatusCode = HttpStatusCode.Forbidden, Message = "No such product category available" };

            return await AddProductToDatabase(addProductRequestDataTransferObject);
        }

        /// <summary>
        /// DeleteProduct
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public async Task<BaseResponse> DeleteProduct(int productId)
        {
            var product = await _productGenericRepository.FirstOrDefault(x => x.Id == productId);

            if(null == product)
                return new BaseResponse { StatusCode = HttpStatusCode.Forbidden,Message="No such product available" };

            await _productGenericRepository.Delete(product);
            return new BaseResponse { StatusCode = HttpStatusCode.OK, Message = "Product Removed Successfully" };
        }

        /// <summary>
        /// GetAllProducts
        /// </summary>
        /// <returns></returns>
        public async Task<BaseResponse> GetAllProducts(PagedParameters pagedParameters)
        {
            var productIncludeListExpression = GetProdcutIncludeList();
            var productOrderBySelector = OrderBySelectors.GetProductOrderBySelector(pagedParameters.OrderBy);
            var allPagedProductsData = await _productGenericRepository.GetAllPagedresultWithIncludes
                (pagedParameters,productOrderBySelector,productIncludeListExpression);

            if (null == allPagedProductsData || !allPagedProductsData.Items.Any())
                return new BaseResponse { StatusCode = HttpStatusCode.NoContent };

            var allPagedProductResponseData = _mapper.Map<IEnumerable<ProductResponseDataTransferObject>>(allPagedProductsData.Items);

            var allPagedProductResponse = new PagedModel<ProductResponseDataTransferObject>
            {
                CurrentPage = allPagedProductsData.CurrentPage,
                PageSize = allPagedProductsData.PageSize,
                Items = allPagedProductResponseData,
                TotalItems = allPagedProductsData.TotalItems
            };

            return new BaseResponse { StatusCode = HttpStatusCode.OK , Data = allPagedProductResponse };
        }

        /// <summary>
        /// ModifyProduct
        /// </summary>
        /// <param name="modifyProductRequestDataTransferObject"></param>
        /// <returns></returns>
        public async Task<BaseResponse> ModifyProduct(ModifyProductRequestDataTransferObject modifyProductRequestDataTransferObject)
        {
            var product = await _productGenericRepository.FirstOrDefault(x => x.Id == modifyProductRequestDataTransferObject.ProductId);

            if (null == product)
                return new BaseResponse { StatusCode = HttpStatusCode.Forbidden, Message = "No such product available" };

            product = _mapper.Map(modifyProductRequestDataTransferObject, product);
            await _productGenericRepository.Update(product);

            return new BaseResponse { StatusCode = HttpStatusCode.OK, Message = "Product Updated Successfully" };
        }

        private IEnumerable<Expression<Func<Product,object>>> GetProdcutIncludeList()
        {
            var prodcutIncludeList = new List<Expression<Func<Product, object>>>();
            Expression<Func<Product, object>> supplierExpression = x => x.Supplier;
            Expression<Func<Product, object>> categoryExpression = x => x.Category;

            prodcutIncludeList.Add(supplierExpression);
            prodcutIncludeList.Add(categoryExpression);

            return prodcutIncludeList;
        }

        private async Task<BaseResponse> AddProductToDatabase(AddProductRequestDataTransferObject addProductRequestDataTransferObject)
        {
            var product = await _productGenericRepository.FirstOrDefault(x => x.ProductName == addProductRequestDataTransferObject.ProductName
            && x.SupplierId == addProductRequestDataTransferObject.SupplierId);

            if (null != product)
                return new BaseResponse { StatusCode = HttpStatusCode.Forbidden, Message = "Product currently available" };

            product = _mapper.Map<Product>(addProductRequestDataTransferObject);
            product.ProductStatus = "Available";
            await _productGenericRepository.Add(product);

            return new BaseResponse { StatusCode = HttpStatusCode.Created, Message = "Product Added Successfully" };
        }
    }
}
