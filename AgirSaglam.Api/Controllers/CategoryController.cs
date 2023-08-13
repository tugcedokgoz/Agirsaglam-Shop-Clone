using AgirSaglam.Model.Models;
using AgirSaglam.Model.View;
using AgirSaglam.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        [HttpGet("GetAllCategory")]
        public dynamic GetAllCategory()
        {
            List<Category> items;

            if (!cache.TryGetValue("GetAllCategory", out items))
            {
                items = repo.CategoryRepository.FindAll().ToList<Category>();
                cache.Set("TumKategoriler", items, DateTimeOffset.UtcNow.AddHours(1));
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
                    message = "Invalid Id"
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

        //admin listeleyeceği Kategori
        [HttpGet("CategoryAdminLists")]
       // [Authorize(Roles = "admin")]
        public dynamic CategoryAdminLists()
        {
            List<V_CategoryAdminList> items = repo.CategoryRepository.CategoryAdminLists();
            return new
            {
                success = true,
                data = items
            };
        }

    }
}
