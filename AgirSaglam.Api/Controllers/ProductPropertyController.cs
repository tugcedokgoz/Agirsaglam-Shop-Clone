using AgirSaglam.Model.Models;
using AgirSaglam.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json.Linq;

namespace AgirSaglam.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductPropertyController : BaseController
    {
        public ProductPropertyController(RepositoryWrapper repo,IMemoryCache cache):base(repo,cache) 
        {
            
        }

        [HttpGet("GetAll")]
        public dynamic GetAll()
        {
            var productProperties = repo.ProductPropertyRepository.FindAll();

            var result = productProperties.Select(pp => new
            {
                Id = pp.Id,
                ProductId = pp.ProductId,
                PropertyId = pp.PropertyId,
                ProductName = pp.Product.Name, 
                PropertyName = pp.Property.Name, 
            });

            return new
            {
                success = true,
                data = result
            };
        }


        [HttpGet("GetByProductId/{productId}")]
        public dynamic GetByProductId(int productId)
        {
            var productProperties = repo.ProductPropertyRepository.GetByProductId(productId);
            return new
            {
                success = true,
                data = productProperties
            };
        }

        [HttpGet("GetByPropertyId/{propertyId}")]
        public dynamic GetByPropertyId(int propertyId)
        {
            var productProperties = repo.ProductPropertyRepository.GetByPropertyId(propertyId);
            return new
            {
                success = true,
                data = productProperties
            };
        }

        [HttpPost("Save")]
        public dynamic Save([FromBody] dynamic model)
        {
            dynamic json = JObject.Parse(model.GetRawText());

            ProductProperty productProperty = new ProductProperty()
            {
                Id = json.Id,
                ProductId = json.ProductId,
                PropertyId = json.PropertyId,
            };

            if (productProperty.Id > 0)
            {
                repo.ProductPropertyRepository.Update(productProperty);
            }
            else
            {
                repo.ProductPropertyRepository.Create(productProperty);
            }

            repo.SaveChanges();
            cache.Remove("ProductProperties");

            return new
            {
                success = true,
                message = "Property saved successfully"
            };
        }

        //silme

        [HttpPost("Delete")]
        public dynamic Delete(int id)
        {
            if (id < 0)
                return new
                {
                    success = false,
                    message = "Invalid Id"
                };
            repo.ProductPropertyRepository.RemoveProperty(id);
            return new
            {
                success = true,
                message = "Deleted"
            };
        }

    }
}
