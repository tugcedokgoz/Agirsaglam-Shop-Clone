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
        public dynamic Save([FromBody] dynamic model)
        {
            dynamic json = JObject.Parse(model.GetRawText());

            PropertyGroup item = new PropertyGroup()
            {
                Id = /*Int32.Parse*/(json.Id),
                Name = json.Name,

            };
            if (item.Id > 0)
                repo.PropertyGroupRepository.Update(item);
            else
                repo.PropertyGroupRepository.Create(item);
            repo.SaveChanges();
            cache.Remove("PropertyGroups");
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
            repo.PropertyGroupRepository.RemovePropertyGroup(id);
            return new
            {
                success = true,
                message = "Deleted"
            };
        }
    }
}
