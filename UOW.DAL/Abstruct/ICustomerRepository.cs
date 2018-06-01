using System.Collections.Generic;
using UOW.DAL.Database;

namespace UOW.DAL.Abstruct
{
    /// <summary>
    /// for extended requirements
    /// will be used when needed
    /// </summary>
    public interface ICustomerRepository : IRepository<Customer>
    {
        /// <summary>
        /// Get customers along with supplier information
        /// </summary>
        /// <param name="pageIndex">Index of page current page</param>
        /// <param name="pageSize">No of customers on a page</param>
        /// <returns>Collection of Customer</returns>
        IEnumerable<Customer> GetCustomers(int pageIndex, int pageSize);
    }
}