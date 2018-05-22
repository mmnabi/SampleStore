
using System.Data.Entity;
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
    }
}
