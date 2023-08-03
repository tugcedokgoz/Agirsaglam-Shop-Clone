using AgirSaglam.Model;
using AgirSaglam.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json.Linq;

namespace AgirSaglam.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseController
    {
        public ProductController(RepositoryWrapper repo,IMemoryCache cache):base(repo,cache)
        {
            
        }

        // categoryId'ye göre ürünleri getirme
        //[HttpGet("GetProductsByCategoryId/{categoryId}")]
        //public dynamic GetProductsByCategoryId(int categoryId)
        //{
        //    var products = repo.ProductRepository.GetProductsByCategoryId(categoryId);
        //    return new
        //    {
        //        success = true,
        //        data = products
        //    };
        //}

        [HttpGet("GetProductsByCategoryId/{categoryId}")]
        public dynamic GetProductsByCategoryId(int categoryId)
        {
            var products = repo.ProductRepository.GetProductsByCategoryId(categoryId);
            return new
            {
                success = true,
                data = products
            };
        }


        //tüm ürünleri getirme
        [HttpGet("GetProducts")]
        public dynamic GetProduct()
        {
            // throw new ApplicationException("test hata");

            List<Product> items;
            if (!cache.TryGetValue("GetProduct", out items))
            {
                items = repo.ProductRepository.FindAll().ToList<Product>();

                cache.Set("GetProduct", items, DateTimeOffset.UtcNow.AddSeconds(20));

                cache.Remove("GetProduct");
            }

            return new
            {
                sucess = true,
                data = items
            };
        }

        //kaydetme-update

        [HttpPost("Save")]

        public dynamic Save([FromBody] dynamic model)
        {
            dynamic json = JObject.Parse(model.GetRawText());

            Product item = new Product()
            {
                Id = json.Id,
                Name = json.Name,
                Price = json.Price,
                DiscountPrice = json.DiscountPrice,
                Amount = json.Amount,
                Description = json.Description,
                Image = json.Image,
            };
            if (item.Id > 0)
                repo.ProductRepository.Update(item);
            else
                repo.ProductRepository.Create(item);
            repo.SaveChanges();
            cache.Remove("Products");
            return new
            {
                success = true
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
            repo.ProductRepository.RemoveProduct(id);
            return new
            {
                success = true,
                message = "Deleted"
            };
        }

        // ürün id'sine göre ilgili özellikleri getirme
        [HttpGet("{productId}/Properties")]
        public dynamic GetPropertiesByProductId(int productId)
        {
            var properties = repo.ProductRepository.GetPropertiesByProductId(productId);
            return new
            {
                success = true,
                data = properties
            };
        }
    }
}
