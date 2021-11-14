using ShopBridge.Models;
using ShopBridge.Repository;
using System.Threading.Tasks;

namespace ShopBridge.BusinessLogic
{
    /// <summary>
    /// CategoryBusinessLogic
    /// </summary>
    public class CategoryBusinessLogic : ICategoryBusinessLogic
    {
        private readonly IGenericRepository<Category> _categoryRepositoryBusinessLogic;

        /// <summary>
        /// Category Business Logic Constuctor
        /// </summary>
        /// <param name="categoryRepositoryBusinessLogic"></param>
        public CategoryBusinessLogic(IGenericRepository<Category> categoryRepositoryBusinessLogic)
        {
            _categoryRepositoryBusinessLogic = categoryRepositoryBusinessLogic;
        }

        /// <summary>
        /// GetProductCategory
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public async Task<Category> GetProductCategory(int categoryId)
        {
            return await _categoryRepositoryBusinessLogic.FirstOrDefault(x => x.Id == categoryId);
        }
    }
}
