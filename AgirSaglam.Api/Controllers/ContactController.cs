using AgirSaglam.Model.Models;
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
    public class ContactController : BaseController
    {
        public ContactController(RepositoryWrapper repo, IMemoryCache cache) : base(repo, cache)
        {

        }
        [HttpGet("GetContact")]
        public dynamic GetContact()
        {
            // throw new ApplicationException("test hata");

            List<Contact> items;
            if (!cache.TryGetValue("GetContact", out items))
            {
                items = repo.ContactRepository.FindAll().ToList<Contact>();

                cache.Set("GetContact", items, DateTimeOffset.UtcNow.AddSeconds(20));

                cache.Remove("GetContact");
            }

            return new
            {
                success = true,
                data = items

            };
        }

        [HttpPost("Save")]
        public dynamic SaveContact([FromBody] dynamic model)
        {
            dynamic json = JObject.Parse(model.GetRawText());

            Contact item = new Contact()
            {
                Id = json.Id,
                Name = json.Name,
                Surname = json.Surname,
                Email = json.Email,
                Text = json.Text,
               
            };

            if (item.Id > 0)
            {
                repo.ContactRepository.Update(item);
            }
            else
            {
                repo.ContactRepository.Create(item);
            }

            repo.SaveChanges();

            cache.Remove("GetContact");

            return new { success = true };
        }
    }
}