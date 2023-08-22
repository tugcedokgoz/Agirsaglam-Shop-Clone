using AgirSaglam.Model.Models;
using AgirSaglam.Model.View;
using AgirSaglam.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json.Linq;
using System.Dynamic;

namespace AgirSaglam.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseController
    {
        public ProductController(RepositoryWrapper repo, IMemoryCache cache) : base(repo, cache)
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
                success = true,
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
            {
                foreach(var categoryId in json.Categories)
                {
                    item.ProductCategories.Add(new ProductCategory()
                    {
                        CategoryId = categoryId
                    });
                }
            }
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


        [HttpGet("GetProductsByName")]
        public dynamic GetProductsByName(string name)
        {
            List<Product> items;
            if (!cache.TryGetValue("GetProductsByName" + name, out items))
            {
                items = repo.ProductRepository.FindByCondition(r => r.Name.Contains(name)).ToList<Product>();

                cache.Set("GetProductsByName" + name, items, DateTimeOffset.UtcNow.AddSeconds(20));
            }

            return new
            {
                success = true,
                data = items
            };
        }

      
        [HttpGet("GetAdminProducts")]
        public dynamic GetAdminProducts()
        {
            var products = repo.ProductRepository.GetProductAdminList();

            return new
            {
                success = true,
                data = products
            };
        }
        [HttpGet("ProductId/{productId}")]
        public ActionResult<dynamic> GetProductById(int productId)
        {
            var product = repo.ProductRepository.FindByCondition(p => p.Id == productId)
                            .SingleOrDefault();

            if (product == null)
            {
                return NotFound();
            }

            return new
            {
                success = true,
                data = product
            };
        }

        //parentcategory id ye göre childlardaki tüm productları getirme
        [HttpGet("GetProductsByParentCategoryId/{parentCategoryId}")]
        public dynamic GetProductsByParentCategoryId(int parentCategoryId)
        {
            var products = repo.ProductRepository.GetProductsByParentCategoryId(parentCategoryId);
            return new
            {
                success = true,
                data = products
            };
        }




    }
}
