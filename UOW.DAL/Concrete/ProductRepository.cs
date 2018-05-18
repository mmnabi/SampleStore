
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using UOW.DAL.Abstruct;
using UOW.DAL.Database;

namespace UOW.DAL.Concrete
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(SampleDBEntities context) : base(context)
        {
        }

        public IEnumerable<Product> GetProductsWithMaxPrice(int count)
        {
            return Entities.Products.OrderByDescending(c => c.UnitPrice).Take(count).ToList();
        }

        public IEnumerable<Product> GetProductsWithSupplier(int pageIndex, int pageSize)
        {
            return Entities.Products
                .Include(c => c.Supplier)
                .OrderBy(c => c.ProductName)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public SampleDBEntities Entities => Context as SampleDBEntities;
    }
}
