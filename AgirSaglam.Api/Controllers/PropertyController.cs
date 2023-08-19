using AgirSaglam.Model.Models;
using AgirSaglam.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace AgirSaglam.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : BaseController
    {
        public PropertyController(RepositoryWrapper repo, IMemoryCache cache) : base(repo, cache)
        {
            
        }

        // Tüm özellikleri getiren 
        [HttpGet("GetAllProperties")]
        public dynamic GetAllProperties()
        {
            var properties = repo.PropertyRepository.FindAll()
                .Select(p => new
                {
                    p.Id,
                    p.Name,
                    p.GroupId,
                    GroupName = p.Group.Name
                })
                .ToList();

            return new
            {
                success = true,
                data = properties
            };
        }
        // İsme göre 
        [HttpGet("GetPropertyByName/{name}")]
        public dynamic GetPropertyByName(string name)
        {
            var property = repo.PropertyRepository.FindByCondition(p => p.Name == name)
                .Select(p => new
                {
                    p.Id,
                    p.Name,
                    p.GroupId,
                    GroupName = p.Group.Name
                })
                .FirstOrDefault();

            if (property != null)
            {
                return new
                {
                    success = true,
                    data = property
                };
            }
            else
            {
                return new
                {
                    success = false,
                    message = "Özellik bulunamadı"
                };
            }
        }

        [HttpPost("SaveProperty")]
        public dynamic SaveProperty([FromBody] Property property)
        {
            if (property == null)
            {
                return new
                {
                    success = false,
                    message = "Invalid property data"
                };
            }

            repo.PropertyRepository.Create(property);
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
            repo.PropertyRepository.RemoveProperty(id);
            return new
            {
                success = true,
                message = "Deleted"
            };
        }

    }
}
