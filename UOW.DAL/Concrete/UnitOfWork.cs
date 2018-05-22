using System.Threading.Tasks;
using UOW.DAL.Abstruct;
using UOW.DAL.Database;

namespace UOW.DAL.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SampleDBEntities _context;

        public UnitOfWork(SampleDBEntities context)
        {
            _context = context;
            Products = new ProductRepository(_context);
            Suppliers = new SupplierRepository(_context);
            Customers = new CustomerRepository(_context);
        }

        public IProductRepository Products { get; }
        public ISupplierRepository Suppliers { get; }
        public ICustomerRepository Customers { get; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}