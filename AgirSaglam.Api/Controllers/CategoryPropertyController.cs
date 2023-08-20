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
    public class CategoryPropertyController : BaseController
    {
        public CategoryPropertyController(RepositoryWrapper repo,IMemoryCache cache):base(repo,cache)
        {
            
        }
        [HttpGet("GetAll")]
        public dynamic GetAll()
        {
            var categoryProperties = repo.CategoryPropertyRepository.FindAll();

            var result = categoryProperties.Select(cp => new
            {
                Id = cp.Id,
                GroupId = cp.GroupId,
                CategoryId = cp.CategoryId,
                GroupName = cp.Group.Name, 
                CategoryName = cp.Category.Name, 
            });

            return new
            {
                success = true,
                data = result
            };
        }

        [HttpGet("GetByCategoryId/{categoryId}")]
        public dynamic GetByCategoryId(int categoryId)
        {
            var categoryPropertyGroups = repo.CategoryPropertyRepository.GetGroupsByCategoryId(categoryId);
            return new
            {
                success = true,
                data = categoryPropertyGroups
            };
        }

        [HttpGet("GetByGroupId/{groupId}")]
        public dynamic GetByGroupId(int groupId)
        {
            var categoryPropertyGroups = repo.CategoryPropertyRepository.GetGroupsByGroupId(groupId);
            return new
            {
                success = true,
                data = categoryPropertyGroups
            };
        }

        [HttpPost("Save")]
        public dynamic Save([FromBody] dynamic model)
        {
            dynamic json = JObject.Parse(model.GetRawText());

            CategoryPropertyGroup categoryProperty = new CategoryPropertyGroup()
            {
                Id = json.Id,
                CategoryId = json.CategoryId,
                GroupId = json.GroupId,
            };

            if (categoryProperty.Id > 0)
            {
                repo.CategoryPropertyRepository.Update(categoryProperty);
            }
            else
            {
                repo.CategoryPropertyRepository.Create(categoryProperty);
            }

            repo.SaveChanges();
            cache.Remove("CategoryPropertyGroups");

            return new
            {
                success = true,
                message = "Property saved successfully"
            };
        }

        [HttpPost("Delete")]
        public dynamic Delete(int id)
        {
            if (id < 0)
                return new
                {
                    success = false,
                    message = "Invalid Id"
                };
            repo.CategoryPropertyRepository.RemoveProperty(id);
            return new
            {
                success = true,
                message = "Deleted"
            };
        }
    }
}
