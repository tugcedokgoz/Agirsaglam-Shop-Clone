using AgirSaglam.Model.Models;
using AgirSaglam.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace AgirSaglam.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillController : BaseController
    {
        public BillController(RepositoryWrapper repo,IMemoryCache cache):base(repo,cache)
        {
            
        }
        [HttpGet("GetBills")]
        public dynamic GetBill()
        {
            List<Bill> items;
            if (!cache.TryGetValue("GetBills", out items))
            {
                items = repo.BillRepository.FindAll()
                    .Include(b => b.Adress) // Include metodu ile Adress'i yanında getiriyoruz
                    .ToList();

                cache.Set("GetBills", items, DateTimeOffset.UtcNow.AddSeconds(20));
            }

            return new
            {
                success = true,
                data = items
            };
        }



        //kaydetme-update

        [HttpPost("Save")]

        public dynamic Save([FromBody] dynamic model)
        {
            dynamic json = JObject.Parse(model.GetRawText());

            Bill item = new Bill()
            {
                Id = json.Id,
                UserId = json.UserId,
                AdressId = json.AdressId,
                Name = json.Name,
                Surname = json.Surname,
                Email = json.Email,
                TcNo = json.TcNo,
                PhoneNo = json.PhoneNo,

            };
            if (item.Id > 0)
                repo.BillRepository.Update(item);
            else
                repo.BillRepository.Create(item);
            repo.SaveChanges();
            cache.Remove("Bills");
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
            repo.BillRepository.RemoveBill(id);
            return new
            {
                success = true,
                message = "Deleted"
            };
        }

        [HttpGet("GetBillsByUserId/{userId}")]
        public async Task<IActionResult> GetBillsByUserId(int userId)
        {
            var bills = await repo.BillRepository.GetBillsByUserId(userId);
            if (bills == null || bills.Count == 0)
            {
                return NotFound();
            }

            return Ok(new
            {
                success = true,
                data = bills
            });
        }

    }
}
