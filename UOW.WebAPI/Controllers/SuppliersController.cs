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
    public class SuppliersController : ApiController
    {
        // GET: api/Suppliers
        private readonly ISupplierService _supplierService;

        public int PageSize = 5;

        public SuppliersController(ISupplierService repo)
        {
            _supplierService = repo;
        }

        [HttpGet]
        public HttpResponseMessage GetSuppliers()
        {
            return Request.CreateResponse(HttpStatusCode.OK, _supplierService.Suppliers.ToList());
        }

        [ResponseType(typeof(SupplierDTO))]
        // GET: api/Suppliers/5
        public HttpResponseMessage GetSupplier(int id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _supplierService.Suppliers.FirstOrDefault(p => p.Id == id));
        }

        [ResponseType(typeof(SupplierDTO))]
        // POST: api/Suppliers
        public async Task<HttpResponseMessage> PostSupplier(SupplierDTO supplier)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            try
            {
                if (supplier.Id != 0) return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad Request");
                int newId = await _supplierService.SaveSupplier(supplier);
                if (newId == -1) { return Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, "Not Saved."); }
                supplier.Id = newId;
                var message = Request.CreateResponse(HttpStatusCode.Created, supplier);
                message.Headers.Location = new Uri(Request.RequestUri + supplier.Id.ToString());
                return message;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [ResponseType(typeof(SupplierDTO))]
        [HttpPut]
        // PUT: api/Suppliers/5
        public async Task<HttpResponseMessage> EditSupplier(int id, SupplierDTO supplier)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            if (id != supplier.Id || id == 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad Request");
            }

            try
            {
                int result = await _supplierService.SaveSupplier(supplier);
                if (result == -1) { return Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, "Not Saved."); }
                if (result == 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found");
                }
                return Request.CreateResponse(HttpStatusCode.OK, supplier);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // DELETE: api/Suppliers/5
        public async Task<HttpResponseMessage> Delete(int id)
        {
            try
            {
                SupplierDTO supplier = await _supplierService.DeleteSupplier(id);
                if (supplier == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found");
                }
                return Request.CreateResponse(HttpStatusCode.OK, supplier);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
