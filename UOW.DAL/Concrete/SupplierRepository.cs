using System.Data.Entity;
using UOW.DAL.Abstruct;
using UOW.DAL.Database;

namespace UOW.DAL.Concrete
{
    public class SupplierRepository : Repository<Supplier>, ISupplierRepository
    {
        public SupplierRepository(DbContext context) : base(context)
        {
        }

        public SampleDBEntities Entities => Context as SampleDBEntities;
    }
}