using ShopBridge.DataTransferObjects;
using System.Threading.Tasks;

namespace ShopBridge.BusinessLogic
{
    /// <summary>
    /// IInventoryBusinessLogic
    /// </summary>
    public interface IInventoryBusinessLogic
    {
        /// <summary>
        /// GetAllProducts
        /// </summary>
        /// <returns></returns>
        Task<BaseResponse> GetAllProducts(PagedParameters pagedParameters);

        /// <summary>
        /// Add New Product
        /// </summary>
        /// <param name="addProductRequestDataTransferObject"></param>
        /// <param name="supplierBussinessLogic"></param>
        /// <param name="categoryBusinessLogic"></param>
        /// <returns></returns>
        Task<BaseResponse> AddNewProduct(AddProductRequestDataTransferObject addProductRequestDataTransferObject,
            ISupplierBussinessLogic supplierBussinessLogic, ICategoryBusinessLogic categoryBusinessLogic);

        /// <summary>
        /// ModifyProduct
        /// </summary>
        /// <returns></returns>
        Task<BaseResponse> ModifyProduct(ModifyProductRequestDataTransferObject modifyProductRequestDataTransferObject);

        /// <summary>
        /// DeleteProduct
        /// </summary>
        /// <returns></returns>
        Task<BaseResponse> DeleteProduct(int productId);
    }
}
