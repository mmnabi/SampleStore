using System.Collections.Generic;
using UOW.BLL.DTOs;

namespace UOW.BLL.Models.ViewModels
{
    public class CustomersListViewModel
    {
        public IEnumerable<CustomerDTO> Customers { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}