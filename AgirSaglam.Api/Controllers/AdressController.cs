using AgirSaglam.Model;
using AgirSaglam.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace AgirSaglam.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdressController : BaseController
    {
        public AdressController(RepositoryWrapper repo,IMemoryCache cache):base(repo,cache)
        {
            
        }
        [HttpGet("GetAdress")]
        public dynamic GetAdress()
        {
            // throw new ApplicationException("test hata");

            List<Adress> items;
            if (!cache.TryGetValue("GetAdress", out items))
            {
                items = repo.AdressRepository.FindAll().ToList<Adress>();

                cache.Set("GetAdress", items, DateTimeOffset.UtcNow.AddSeconds(20));

                cache.Remove("GetAdress");
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

            Adress item = new Adress()
            {
                Id = json.Id,
                City = json.City,
                District=json.District,
                PostCode= json.PostCode,

            };
            if (item.Id > 0)
                repo.AdressRepository.Update(item);
            else
                repo.AdressRepository.Create(item);
            repo.SaveChanges();
            cache.Remove("Adress");
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
            repo.AdressRepository.RemoveAdress(id);
            return new
            {
                success = true,
                message = "Deleted"
            };
        }
    }
}
