using System.Collections.Generic;
using UOW.BLL.DTOs;

namespace UOW.BLL.Models.ViewModels
{
    public class ProductsListViewModel
    {
        public IEnumerable<ProductDTO> Products { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}