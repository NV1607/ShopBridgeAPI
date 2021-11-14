using Microsoft.AspNetCore.Mvc;
using ShopBridge.BusinessLogic;
using ShopBridge.DataTransferObjects;
using System.Threading.Tasks;

namespace ShopBridge.Controllers
{
    /// <summary>
    /// InventoryController
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryBusinessLogic _inventoryBusinessLogic;

        /// <summary>
        /// InventoryController Constuctor
        /// </summary>
        public InventoryController(IInventoryBusinessLogic inventoryBusinessLogic)
        {
            _inventoryBusinessLogic = inventoryBusinessLogic;
        }

        /// <summary>
        /// Get All Products In Inventory
        /// </summary>
        /// <param name="pagedParamets"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllProducts([FromQuery]PagedParameters pagedParamets)
        {
           var  response = await _inventoryBusinessLogic.GetAllProducts(pagedParamets);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Add New Product In Inventory
        /// </summary>
        /// <param name="addProductRequestDataTransferObject"></param>
        /// <param name="supplierBussinessLogic"></param>
        /// <param name="categoryBusinessLogic"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddNewProduct(AddProductRequestDataTransferObject addProductRequestDataTransferObject,
            [FromServices] ISupplierBussinessLogic supplierBussinessLogic,[FromServices] ICategoryBusinessLogic categoryBusinessLogic)
        {
            if (!ModelState.IsValid || string.IsNullOrEmpty(addProductRequestDataTransferObject.ProductName))
                return BadRequest(ModelState);

            var response = await _inventoryBusinessLogic.AddNewProduct(addProductRequestDataTransferObject,supplierBussinessLogic,
                categoryBusinessLogic);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Modify Product In Inventory
        /// </summary>
        /// <param name="modifyProductRequestDataTransferObject"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateProduct(ModifyProductRequestDataTransferObject modifyProductRequestDataTransferObject)
        {
            if (!ModelState.IsValid || modifyProductRequestDataTransferObject.ProductId<=0)
                return BadRequest(ModelState);

            var response = await _inventoryBusinessLogic.ModifyProduct(modifyProductRequestDataTransferObject);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Remove Product from Inventory
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> RemoveProduct([FromQuery] int productId)
        {
            if (productId<=0)
                return BadRequest(ModelState);

            var response = await _inventoryBusinessLogic.DeleteProduct(productId);
            return StatusCode((int)response.StatusCode, response);
        }
    }
}
