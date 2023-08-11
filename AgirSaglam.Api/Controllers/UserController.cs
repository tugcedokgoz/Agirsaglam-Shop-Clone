using AgirSaglam.Api.Code.Validations;
using AgirSaglam.Model.Models;
using AgirSaglam.Model.View;
using AgirSaglam.Repository;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json.Linq;

namespace AgirSaglam.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        public UserController(RepositoryWrapper repo, IMemoryCache cache) : base(repo, cache)
        {

        }

        //tüm ürünleri getirme
        [HttpGet("GetUsers")]
        public dynamic GetUsers()
        {
            List<User> users;
            if (!cache.TryGetValue("GetUsers", out users))
            {
                users = repo.UserRepository.FindAll().ToList<User>();

                foreach (var user in users)
                {
                    var (role, adress) = repo.UserRepository.GetUserRoleAndAdressById(user.Id).Result;
                    user.Role = role;
                    user.Adress = adress;
                }

                cache.Set("GetUsers", users, DateTimeOffset.UtcNow.AddSeconds(20));
                cache.Remove("GetUsers");
            }

            return new
            {
                success = true,
                data = users
            };
        }


        //kaydetme-update
        [HttpPost("Save")]
        public dynamic Save([FromBody] dynamic model)
        {
            dynamic json = JObject.Parse(model.GetRawText());

            User item = new User()
            {
                Id = json.Id,
                UserName = json.UserName,
                Email = json.Email,
                Password = json.Password,
                CreateDate = json.CreateDate,
                UpdateDate = json.UpdateDate,
                EmailConfirm = json.EmailConfirm,
                EmailConfirmDate = json.EmailConfirmDate,
                Status = json.Status,
                RoleId = json.RoleId,
                AdressId = json.AdressId,
            };
            if (item.Id > 0)
                repo.UserRepository.Update(item);
            else
                repo.UserRepository.Create(item);
            repo.SaveChanges();
            cache.Remove("Users");
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
            repo.UserRepository.RemoveUser(id);
            return new
            {
                success = true,
                message = "Deleted"
            };
        }

        //id ye göre getirme
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            User user = await repo.UserRepository.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                success = true,
                data = new
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

                    },
                    Role = new
                    {
                        user.Role?.Id,
                        user.Role?.Name

                    }
                }
            });
        }

        //kullanıcı id ye rol getirme
        [HttpGet("{id}/Role")]
        public async Task<IActionResult> GetUserRoleById(int id)
        {
            var userRole = await repo.UserRepository.GetUserRoleById(id);
            if (userRole == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                success = true,
                data = userRole
            });
        }

        //kullanıcı id ye adress getirme
        [HttpGet("{id}/Adress")]
        public async Task<IActionResult> GetUserAdressById(int id)
        {
            var userAdress = await repo.UserRepository.GetUserAdressById(id);
            if (userAdress == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                success = true,
                data = userAdress
            });
        }

        //View Aktif kullanici getirme
        [Authorize]
        [HttpGet("AktiveUsers")]
        public dynamic AktiveUsers()
        {
            List<V_AktiveUsers> items = repo.UserRepository.GetAktiveUsers();

            // Nullable alanların kontrolü
            var filteredItems = items.Where(item =>
                !string.IsNullOrEmpty(item.Email) &&
                item.CreateDate != null
            ).ToList();

            return new
            {
                success = true,
                data = filteredItems
            };
        }



        ////üye ol
        [HttpPost("SignUp")]
        public dynamic SignUp([FromBody] dynamic model)
        {
            dynamic json = JObject.Parse(model.GetRawText());

            string userName = json.UserName;
            string password = json.Password;

            User item = new User()
            {
                Status = true,
                UserName = userName,
                Password = password,
                RoleId = Enums.Roles.Kullanici
            };
            User user=repo.UserRepository.FindByCondition(u=>u.UserName==item.UserName).SingleOrDefault<User>();
            if (user !=null)
            {
                return new
                {
                    success = false,
                    message = "bu kullanıcı adı zaten kullanılıyor."
                };
            }

            repo.UserRepository.Create(item);
            repo.SaveChanges();

            return new
            {
                success = true
            };
        }

        [HttpGet("GetUsersByName")]
        public dynamic GetUsersByName(string userName)
        {
            List<User> items;
            if (!cache.TryGetValue("GetUsersByName" + userName, out items))
            {
                items = repo.UserRepository.FindByCondition(r => r.UserName.Contains(userName)).ToList<User>();

                cache.Set("GetUsersByName" + userName, items, DateTimeOffset.UtcNow.AddSeconds(20));
            }

            return new
            {
                success = true,
                data = items
            };
        }
    }
}
