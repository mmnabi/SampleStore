
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using UOW.DAL.Abstruct;
using UOW.DAL.Database;

namespace UOW.DAL.Concrete
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(DbContext context) : base(context)
        {
        }

        public SampleDBEntities Entities => Context as SampleDBEntities;

        public IEnumerable<Customer> GetCustomers(int pageIndex, int pageSize)
        {
            return Entities.Customers
                .OrderBy(c => c.Id)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }
    }
}
