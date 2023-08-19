using AgirSaglam.Model.Models;
using AgirSaglam.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

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
            var categoryPropertyGroups = repo.CategoryPropertyRepository.FindAll();
            return new
            {
                success = true,
                data = categoryPropertyGroups
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
        public dynamic SaveProperty([FromBody] CategoryPropertyGroup categoryPropertyGroup)
        {
            if (categoryPropertyGroup == null)
            {
                return new
                {
                    success = false,
                    message = "Invalid property group data"
                };
            }

            repo.CategoryPropertyRepository.Create(categoryPropertyGroup);
            repo.SaveChanges();

            return new
            {
                success = true,
                message = "Property group saved successfully"
            };
        }

        [HttpPost("Delete")]
        public dynamic Delete(int categoryId,int groupId)
        {
            if (categoryId <= 0 || groupId <= 0)
            {
                return new
                {
                    success = false,
                    message = "Invalid categoryId or groupId"
                };
            }

            repo.CategoryPropertyRepository.RemoveCategoryPropertyGroup(categoryId, groupId);
            repo.SaveChanges();

            return new
            {
                success = true,
                message = "Deleted"
            };
        }
    }
}
