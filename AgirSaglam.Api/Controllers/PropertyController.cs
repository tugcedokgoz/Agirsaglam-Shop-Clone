using AgirSaglam.Model.Models;
using AgirSaglam.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

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
        public ActionResult GetPropertyByName(string name)
        {
            try
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
                    return Ok(new
                    {
                        success = true,
                        data = property
                    });
                }
                else
                {
                    return NotFound(new
                    {
                        success = false,
                        message = "Özellik bulunamadı"
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "Sunucu hatası: " + ex.Message
                });
            }
        }





        [HttpPost("Save")]
        public dynamic Save([FromBody] dynamic model)
        {
            dynamic json = JObject.Parse(model.GetRawText());


            Property item = new Property()
            {
                Id = json.Id,
                Name = json.Name,
                GroupId = json.GroupId,


            };
            if (item.Id > 0)
                repo.PropertyRepository.Update(item);
            else
                repo.PropertyRepository.Create(item);
            Console.WriteLine(item);
            Console.WriteLine(repo);
            repo.SaveChanges();
            cache.Remove("Properties");
            return new
            {
                success = true,
                message="doğru"
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
