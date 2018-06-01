using System.Collections.Generic;
using System.Threading.Tasks;
using UOW.BLL.DTOs;
using UOW.BLL.Models.ViewModels;

namespace UOW.BLL.Abstruct
{
    public interface ICustomerService
    {
        IEnumerable<CustomerDTO> Customers { get; }
        CustomersListViewModel GetCustomers(int pageIndex, int pageSize);
        Task<int> SaveCustomer(CustomerDTO customer);
        Task<CustomerDTO> DeleteCustomer(int customerId);
    }
}