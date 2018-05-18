using System.Collections.Generic;
using UOW.DAL.Database;

namespace UOW.DAL.Abstruct
{
    /// <summary>
    /// for extended requirements
    /// will be used when needed
    /// </summary>
    public interface IProductRepository : IRepository<Product>
    {
        /// <summary>
        /// Get limited no of products order by max price
        /// </summary>
        /// <param name="count">how many products to fetch</param>
        /// <returns>Collection of Product</returns>
        IEnumerable<Product> GetProductsWithMaxPrice(int count);

        /// <summary>
        /// Get products along with supplier information
        /// </summary>
        /// <param name="pageIndex">Index of page current page</param>
        /// <param name="pageSize">No of products on a page</param>
        /// <returns>Collection of Product</returns>
        IEnumerable<Product> GetProductsWithSupplier(int pageIndex, int pageSize);
    }
}