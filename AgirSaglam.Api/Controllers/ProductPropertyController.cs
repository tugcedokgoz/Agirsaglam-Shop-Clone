using AgirSaglam.Model.Models;
using AgirSaglam.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

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
            return new
            {
                success = true,
                data = productProperties
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
        public dynamic SaveProperty([FromBody] ProductProperty productProperty)
        {
            if (productProperty == null)
            {
                return new
                {
                    success = false,
                    message = "Invalid property data"
                };
            }

            repo.ProductPropertyRepository.Create(productProperty);
            repo.SaveChanges();

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
