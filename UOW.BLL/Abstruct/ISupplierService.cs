using System.Collections.Generic;
using System.Threading.Tasks;
using UOW.BLL.DTOs;

namespace UOW.BLL.Abstruct
{
    public interface ISupplierService
    {
        IEnumerable<SupplierDTO> Suppliers { get; }
        Task<int> SaveSupplier(SupplierDTO supplier);
        Task<SupplierDTO> DeleteSupplier(int supplierId);
    }
}