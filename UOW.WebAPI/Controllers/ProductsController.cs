using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using UOW.BLL.Abstruct;
using UOW.BLL.DTOs;
using UOW.BLL.Models.ViewModels;

namespace UOW.WebAPI.Controllers
{
    public class ProductsController : ApiController
    {
        // GET: api/Products
        private readonly IProductService _productService;

        public int PageSize = 5;

        public ProductsController(IProductService repo)
        {
            _productService = repo;
        }

        [HttpGet]
        public HttpResponseMessage GetProducts()
        {
            return Request.CreateResponse(HttpStatusCode.OK, _productService.Products.ToList());
        }

        [ResponseType(typeof(ProductsListViewModel))]
        [Route("api/Products/Page{page}")]
        [HttpGet]
        public HttpResponseMessage GetProductsWithSupplier(int page = 1)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _productService.GetProductsWithSupplier(page, PageSize));
        }

        [ResponseType(typeof(ProductDTO))]
        // GET: api/Products/5
        public HttpResponseMessage GetProduct(int id)
        {
            ProductDTO product = _productService.Products.FirstOrDefault(p => p.Id == id);
            if (product == null) return Request.CreateResponse(HttpStatusCode.NotFound, "Not Found.");
            return Request.CreateResponse(HttpStatusCode.OK, product);
        }

        [ResponseType(typeof(ProductDTO))]
        // POST: api/Products
        public async Task<HttpResponseMessage> PostProduct(ProductDTO product)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            try
            {
                if (product.Id != 0) return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad Request");
                int newId = await _productService.SaveProduct(product);
                if (newId == -1) { return Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, "Not Saved."); }
                product.Id = newId;
                var message = Request.CreateResponse(HttpStatusCode.Created, product);
                message.Headers.Location = new Uri(Request.RequestUri + product.Id.ToString());
                return message;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [ResponseType(typeof(ProductDTO))]
        [HttpPut]
        // PUT: api/Products/5
        public async Task<HttpResponseMessage> EditProduct(int id, ProductDTO product)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            if (id != product.Id || id == 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad Request");
            }

            try
            {
                int result = await _productService.SaveProduct(product);
                if (result == -1) { return Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, "Not Saved."); }
                if (result == 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found");
                }
                return Request.CreateResponse(HttpStatusCode.OK, product);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // DELETE: api/Products/5
        public async Task<HttpResponseMessage> Delete(int id)
        {
            try
            {
                ProductDTO product = await _productService.DeleteProduct(id);
                if (product == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found");
                }
                return Request.CreateResponse(HttpStatusCode.OK, product);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
