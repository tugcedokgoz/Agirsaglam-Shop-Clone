using AgirSaglam.Model.Models;
using AgirSaglam.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json.Linq;

namespace AgirSaglam.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : BaseController
    {
        public CategoryController(RepositoryWrapper repo, IMemoryCache cache) : base(repo, cache)
        {

        }
        //tüm kategorileri getiren 

        [HttpGet("GetCategories")]
        public dynamic GetCategories()
        {
            List<Category> items;
            if (!cache.TryGetValue("GetCategory", out items))
            {
                items = repo.CategoryRepository.FindAll()
                    .Include(c => c.ParentCategory) // Include parent category data
                    .ToList<Category>();

                cache.Set("GetCategory", items, DateTimeOffset.UtcNow.AddSeconds(20));

                cache.Remove("GetCategory");
            }

            return new
            {
                success = true,
                data = items
            };
        }

        //idye göre getiren

        [HttpGet("{id}")]
        public dynamic Get(int id)
        {
            Category items = repo.CategoryRepository.FindByCondition(a => a.Id == id).SingleOrDefault<Category>();
            return new
            {
                success = true,
                data = items
            };
        }

        //Parentkategori getirme
        [HttpGet("ParentCategory")]
        public dynamic ParentCategory()
        {
            List<Category> items = repo.CategoryRepository.FindByCondition(a => a.ParentCategoryId == null).ToList<Category>();
            return new
            {
                success = true,
                data = items
            };
        }

        //parentID göre child getirme

        [HttpGet("GetChildIdByParentCategory/{id}")]
        public dynamic GetChildIdByParentCategory(int id)
        {
            List<Category> items = repo.CategoryRepository.FindByCondition(a => a.ParentCategoryId != null && a.ParentCategoryId == id).ToList<Category>();
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

            Category item = new Category()
            {
                Id = json.Id,
                Name = json.Name,
                Status = json.Status,
                ParentCategoryId = json.ParentCategoryId
            };
            if (item.Id > 0)
                repo.CategoryRepository.Update(item);
            else
                repo.CategoryRepository.Create(item);
            repo.SaveChanges();
            cache.Remove("Categories");
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
                    message="Invalid Id"
                };
            repo.CategoryRepository.RemoveCategory(id);
            return new
            {
                success = true,
                message = "Deleted"
            };
        }

        // categoryId'ye göre PropertyGroup listesini getiren method
        [HttpGet("{categoryId}/PropertyGroups")]
        public dynamic GetPropertyGroupsByCategoryId(int categoryId)
        {
            var propertyGroups = repo.CategoryRepository.GetPropertyGroupsByCategoryId(categoryId);

            // dynamic olarak dönüş yapalım
            return new
            {
                success = true,
                data = propertyGroups
            };
        }

    }
}
