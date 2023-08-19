using AgirSaglam.Model.Models;
using AgirSaglam.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace AgirSaglam.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyGroupController : BaseController
    {
        public PropertyGroupController(RepositoryWrapper repo ,IMemoryCache cache):base(repo,cache)
        {
            
        }
        [HttpGet("GetAll")]
        public dynamic GetAllPropertyGroups()
        {
            var propertyGroups = repo.PropertyGroupRepository.FindAll().ToList();
            return new
            {
                success = true,
                data = propertyGroups
            };
        }
        [HttpGet("GetByName")]
        public dynamic GetByName(string name)
        {
            List<PropertyGroup> items;
            if (!cache.TryGetValue("GetByName_" + name, out items))
            {
                items = repo.PropertyGroupRepository.FindByCondition(r => r.Name.Contains(name)).ToList<PropertyGroup>();

                cache.Set("GetByName_" + name, items, DateTimeOffset.UtcNow.AddSeconds(20));
            }

            return new
            {
                success = true,
                data = items
            };
        }

        [HttpPost("Save")]
        public dynamic SavePropertyGroup([FromBody] PropertyGroup propertyGroup)
        {
            try
            {
                if (propertyGroup == null)
                {
                    return new
                    {
                        success = false,
                        message = "Invalid data"
                    };
                }

                if (ModelState.IsValid)
                {
                    repo.PropertyGroupRepository.Create(propertyGroup);
                    repo.SaveChanges();

                    return new
                    {
                        success = true,
                        message = "Property grubu başarıyla kaydedildi"
                    };
                }
                else
                {
                    return new
                    {
                        success = false,
                        message = "Validation error",
                        errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage))
                    };
                }
            }
            catch (Exception ex)
            {
                return new
                {
                    success = false,
                    message = "Property grubu kaydedilirken bir hata oluştu",
                    error = ex.Message
                };
            }
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
            repo.PropertyGroupRepository.RemovePropertyGroup(id);
            return new
            {
                success = true,
                message = "Deleted"
            };
        }
    }
}
