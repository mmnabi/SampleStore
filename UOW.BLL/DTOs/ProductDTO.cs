using System.Collections.Generic;

namespace UOW.BLL.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int SupplierId { get; set; }
        public decimal? UnitPrice { get; set; }
        public string Package { get; set; }
        public bool IsDiscontinued { get; set; }

        public virtual IEnumerable<OrderItemDTO> OrderItems { get; set; }
        public virtual SupplierDTO Supplier { get; set; }
    }
}