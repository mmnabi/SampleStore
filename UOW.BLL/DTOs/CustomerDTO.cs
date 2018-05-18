using System.Collections.Generic;

namespace UOW.BLL.DTOs
{
    public class CustomerDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public virtual IEnumerable<OrderDTO> Orders { get; set; }
    }
}