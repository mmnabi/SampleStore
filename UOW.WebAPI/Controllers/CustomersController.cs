using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using UOW.BLL.Abstruct;
using UOW.BLL.DTOs;

namespace UOW.WebAPI.Controllers
{
    public class CustomersController : ApiController
    {
        // GET: api/Customers
        private readonly ICustomerService _customerService;

        public int PageSize = 5;

        public CustomersController(ICustomerService repo)
        {
            _customerService = repo;
        }

        [HttpGet]
        public HttpResponseMessage GetCustomers()
        {
            return Request.CreateResponse(HttpStatusCode.OK, _customerService.Customers.ToList());
        }

        [ResponseType(typeof(CustomerDTO))]
        // GET: api/Customers/5
        public HttpResponseMessage GetCustomer(int id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _customerService.Customers.FirstOrDefault(p => p.Id == id));
        }

        [ResponseType(typeof(CustomerDTO))]
        // POST: api/Customers
        public async Task<HttpResponseMessage> PostCustomer(CustomerDTO customer)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            try
            {
                if (customer.Id != 0) return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad Request");
                int newId = await _customerService.SaveCustomer(customer);
                if (newId == -1) { return Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, "Not Saved."); }
                customer.Id = newId;
                var message = Request.CreateResponse(HttpStatusCode.Created, customer);
                message.Headers.Location = new Uri(Request.RequestUri + customer.Id.ToString());
                return message;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [ResponseType(typeof(CustomerDTO))]
        [HttpPut]
        // PUT: api/Customers/5
        public async Task<HttpResponseMessage> EditCustomer(int id, CustomerDTO customer)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            if (id != customer.Id)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad Request");
            }

            try
            {
                int result = await _customerService.SaveCustomer(customer);
                if (result == -1) { return Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, "Not Saved."); }
                if (result == 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found");
                }
                return Request.CreateResponse(HttpStatusCode.OK, customer);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // DELETE: api/Customers/5
        public async Task<HttpResponseMessage> Delete(int id)
        {
            try
            {
                CustomerDTO customer = await _customerService.DeleteCustomer(id);
                if (customer == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found");
                }
                return Request.CreateResponse(HttpStatusCode.OK, customer);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
