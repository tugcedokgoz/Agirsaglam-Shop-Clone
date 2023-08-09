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
    public class RoleController : BaseController
    {
        public RoleController(RepositoryWrapper repo, IMemoryCache cache) : base(repo, cache)
        {

        }
        [HttpGet("GetRoles")]
        public dynamic GetRole()
        {
            // throw new ApplicationException("test hata");

            List<Role> items;
            if (!cache.TryGetValue("GetRole", out items))
            {
                items = repo.RoleRepository.FindAll().ToList<Role>();

                cache.Set("GetRole", items, DateTimeOffset.UtcNow.AddSeconds(20));

                cache.Remove("GetRole");
            }

            return new
            {
                sucess = true,
                data = items
            };
        }


        //id ye göre getirme
        [HttpGet("{id}")]
        public dynamic Get(int id)
        {
            Role items = repo.RoleRepository.FindByCondition(a => a.Id == id).SingleOrDefault<Role>();
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

            Role item = new Role()
            {
                Id = json.Id,
                Name = json.Name,
               
            };
            if (item.Id > 0)
                repo.RoleRepository.Update(item);
            else
                repo.RoleRepository.Create(item);
            repo.SaveChanges();
            cache.Remove("Roles");
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
            repo.RoleRepository.RemoveRole(id);
            return new
            {
                success = true,
                message = "Deleted"
            };
        }

        //roleId ye göre user getirme
        [HttpGet("{roleId}/Users")]
        public async Task<IActionResult> GetUsersByRoleId(int roleId)
        {
            var users = await repo.RoleRepository.GetUsersByRoleId(roleId);
            if (users == null || users.Count == 0)
            {
                return NotFound();
            }

            var usersWithAdress = users.Select(user => new
            {
                user.Id,
                user.UserName,
                user.Email,
                user.CreateDate,
                user.UpdateDate,
                user.EmailConfirm,
                user.EmailConfirmDate,
                user.Status,
                Adress = new
                {
                    user.Adress?.Id,
                    user.Adress?.City,
                    user.Adress?.District,
                    user.Adress?.PostCode
                    // Adress nesnesinin diğer verilerini buraya ekleyebilirsiniz
                }
            }).ToList();

            return Ok(new
            {
                success = true,
                data = usersWithAdress
            });
        }

    }
}
