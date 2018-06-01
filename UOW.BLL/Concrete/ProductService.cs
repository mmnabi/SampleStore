using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UOW.BLL.Abstruct;
using UOW.BLL.DTOs;
using UOW.BLL.Models;
using UOW.BLL.Models.ViewModels;
using UOW.DAL.Concrete;
using UOW.DAL.Database;

namespace UOW.BLL.Concrete
{
    public class ProductService : IProductService
    {
        private readonly UnitOfWork _unitOfWork;

        public ProductService()
        {
            _unitOfWork = new UnitOfWork(new SampleDBEntities());
        }

        public IEnumerable<ProductDTO> Products
        {
            get
            {
                return _unitOfWork.Products
                    .GetAll()
                    .Select(product => new ProductDTO
                    {
                        Id = product.Id,
                        ProductName = product.ProductName,
                        SupplierId = product.SupplierId,
                        UnitPrice = product.UnitPrice,
                        Package = product.Package,
                        IsDiscontinued = product.IsDiscontinued
                    });
            }
        }

        public ProductsListViewModel GetProductsWithSupplier(int pageIndex, int pageSize)
        {
            ProductsListViewModel model = new ProductsListViewModel
            {
                Products = _unitOfWork.Products
                    .GetProductsWithSupplier(pageIndex, pageSize)
                    .Select(product => new ProductDTO
                    {
                        Id = product.Id,
                        ProductName = product.ProductName,
                        Supplier = new SupplierDTO
                        {
                            Id = product.Supplier.Id,
                            CompanyName = product.Supplier.CompanyName
                        },
                        UnitPrice = product.UnitPrice
                    }),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = pageIndex,
                    ItemsPerPage = pageSize,
                    TotalItems = _unitOfWork.Products.Count()
                }
            };
            return model;
        }

        public async Task<int> SaveProduct(ProductDTO product)
        {
            Product dto = new Product();
            if (product.Id == 0)
            {
                dto.IsDiscontinued = product.IsDiscontinued;
                dto.ProductName = product.ProductName;
                dto.Package = product.Package;
                dto.SupplierId = product.SupplierId;
                dto.UnitPrice = product.UnitPrice;
                _unitOfWork.Products.Add(dto);
            }
            else
            {
                dto = _unitOfWork.Products.Get(product.Id);
                if (dto != null)
                {
                    dto.IsDiscontinued = product.IsDiscontinued;
                    dto.ProductName = product.ProductName;
                    dto.Package = product.Package;
                    dto.SupplierId = product.SupplierId;
                    dto.UnitPrice = product.UnitPrice;
                }
            }
            await _unitOfWork.CompleteAsync();
            if (dto != null) return dto.Id;
            return -1;
        }

        public async Task<ProductDTO> DeleteProduct(int productId)
        {
            Product product = _unitOfWork.Products.Get(productId);
            ProductDTO dto = new ProductDTO();

            if (product == null)
            {
                return null;
            }
            _unitOfWork.Products.Remove(product);
            dto.Id = product.Id;
            dto.IsDiscontinued = product.IsDiscontinued;
            dto.ProductName = product.ProductName;
            dto.Package = product.Package;
            dto.SupplierId = product.SupplierId;
            dto.UnitPrice = product.UnitPrice;
            await _unitOfWork.CompleteAsync();
            return dto;
        }
    }
}