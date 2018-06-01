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
    public class CustomerService : ICustomerService
    {
        private readonly UnitOfWork _unitOfWork;

        public CustomerService()
        {
            _unitOfWork = new UnitOfWork(new SampleDBEntities());
        }

        public IEnumerable<CustomerDTO> Customers
        {
            get
            {
                return _unitOfWork.Customers
                    .GetAll()
                    .Select(customer => new CustomerDTO
                    {
                        Id = customer.Id,
                        FirstName = customer.FirstName,
                        LastName = customer.LastName,
                        City = customer.City,
                        Country = customer.Country,
                        Phone = customer.Phone
                    }).ToList();
            }
        }

        public CustomersListViewModel GetCustomers(int pageIndex, int pageSize)
        {
            CustomersListViewModel model = new CustomersListViewModel
            {
                Customers = _unitOfWork.Customers
                    .GetCustomers(pageIndex, pageSize)
                    .Select(customer => new CustomerDTO
                    {
                        Id = customer.Id,
                        FirstName = customer.FirstName,
                        LastName = customer.LastName,
                        City = customer.City,
                        Country = customer.Country,
                        Phone = customer.Phone
                    }),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = pageIndex,
                    ItemsPerPage = pageSize,
                    TotalItems = _unitOfWork.Customers.Count()
                }
            };
            return model;
        }

        public async Task<int> SaveCustomer(CustomerDTO customer)
        {
            Customer dto = new Customer();
            if (customer.Id == 0)
            {
                dto.FirstName = customer.FirstName;
                dto.LastName = customer.LastName;
                dto.City = customer.City;
                dto.Country = customer.Country;
                dto.Phone = customer.Phone;
                _unitOfWork.Customers.Add(dto);
            }
            else
            {
                dto = _unitOfWork.Customers.Get(customer.Id);
                if (dto != null)
                {
                    dto.FirstName = customer.FirstName;
                    dto.LastName = customer.LastName;
                    dto.City = customer.City;
                    dto.Country = customer.Country;
                    dto.Phone = customer.Phone;
                }
            }
            await _unitOfWork.CompleteAsync();
            if (dto != null) return dto.Id;
            return -1;
        }

        public async Task<CustomerDTO> DeleteCustomer(int customerId)
        {
            Customer customer = _unitOfWork.Customers.Get(customerId);
            CustomerDTO dto = new CustomerDTO();

            if (customer == null)
            {
                return null;
            }
            _unitOfWork.Customers.Remove(customer);
            dto.Id = customer.Id;
            dto.FirstName = customer.FirstName;
            dto.LastName = customer.LastName;
            dto.City = customer.City;
            dto.Country = customer.Country;
            dto.Phone = customer.Phone;
            await _unitOfWork.CompleteAsync();
            return dto;
        }
    }
}