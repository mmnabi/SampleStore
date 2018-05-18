using System.Collections.Generic;

namespace UOW.BLL.DTOs
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public System.DateTime OrderDate { get; set; }
        public string OrderNumber { get; set; }
        public int CustomerId { get; set; }
        public decimal? TotalAmount { get; set; }

        public virtual CustomerDTO Customer { get; set; }
        public virtual IEnumerable<OrderItemDTO> OrderItems { get; set; }
    }
}