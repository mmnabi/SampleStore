using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UOW.BLL.Abstruct;
using UOW.BLL.DTOs;
using UOW.DAL.Concrete;
using UOW.DAL.Database;

namespace UOW.BLL.Concrete
{
    public class SupplierService : ISupplierService
    {
        private readonly UnitOfWork _unitOfWork;

        public SupplierService()
        {
            _unitOfWork = new UnitOfWork(new SampleDBEntities());
        }

        public IEnumerable<SupplierDTO> Suppliers
        {
            get
            {
                return _unitOfWork.Suppliers
                    .GetAll()
                    .Select(supplier => new SupplierDTO
                    {
                        Id = supplier.Id,
                        CompanyName = supplier.CompanyName,
                        ContactName = supplier.ContactName,
                        ContactTitle = supplier.ContactTitle,
                        City = supplier.City,
                        Country = supplier.Country,
                        Phone = supplier.Phone,
                        Fax = supplier.Fax
                    }).ToList();
            }
        }

        public async Task<int> SaveSupplier(SupplierDTO supplier)
        {
            Supplier dto = new Supplier();
            if (supplier.Id == 0)
            {
                dto.CompanyName = supplier.CompanyName;
                dto.ContactName = supplier.ContactName;
                dto.ContactTitle = supplier.ContactTitle;
                dto.City = supplier.City;
                dto.Country = supplier.Country;
                dto.Phone = supplier.Phone;
                dto.Fax = supplier.Fax;
                _unitOfWork.Suppliers.Add(dto);
            }
            else
            {
                dto = _unitOfWork.Suppliers.Get(supplier.Id);
                if (dto != null)
                {
                    dto.CompanyName = supplier.CompanyName;
                    dto.ContactName = supplier.ContactName;
                    dto.ContactTitle = supplier.ContactTitle;
                    dto.City = supplier.City;
                    dto.Country = supplier.Country;
                    dto.Phone = supplier.Phone;
                    dto.Fax = supplier.Fax;
                }
            }
            await _unitOfWork.CompleteAsync();
            if (dto != null) return dto.Id;
            return -1;
        }

        public async Task<SupplierDTO> DeleteSupplier(int supplierId)
        {
            Supplier supplier = _unitOfWork.Suppliers.Get(supplierId);
            SupplierDTO dto = new SupplierDTO();

            if (supplier == null)
            {
                return null;
            }
            _unitOfWork.Suppliers.Remove(supplier);
            dto.Id = supplier.Id;
            dto.CompanyName = supplier.CompanyName;
            dto.ContactName = supplier.ContactName;
            dto.ContactTitle = supplier.ContactTitle;
            dto.City = supplier.City;
            dto.Country = supplier.Country;
            dto.Phone = supplier.Phone;
            dto.Fax = supplier.Fax;
            await _unitOfWork.CompleteAsync();
            return dto;
        }
    }
}