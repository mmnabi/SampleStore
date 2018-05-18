using System.Collections.Generic;
using System.Threading.Tasks;
using UOW.BLL.DTOs;
using UOW.BLL.Models.ViewModels;

namespace UOW.BLL.Abstruct
{
    public interface IProductService
    {
        IEnumerable<ProductDTO> ProductoDtos { get; }
        ProductsListViewModel GetProductsWithSupplier(int pageIndex, int pageSize);
        Task<int> SaveProduct(ProductDTO product);
        Task<ProductDTO> DeleteProduct(int productId);
    }
}