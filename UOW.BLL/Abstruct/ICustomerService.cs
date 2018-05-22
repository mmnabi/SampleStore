using System.Collections.Generic;
using System.Threading.Tasks;
using UOW.BLL.DTOs;

namespace UOW.BLL.Abstruct
{
    public interface ICustomerService
    {
        IEnumerable<CustomerDTO> Customers { get; }
        Task<int> SaveCustomer(CustomerDTO customer);
        Task<CustomerDTO> DeleteCustomer(int customerId);
    }
}