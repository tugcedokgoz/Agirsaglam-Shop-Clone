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
    public class OrderController : BaseController
    {
        public OrderController(RepositoryWrapper repo, IMemoryCache cache) : base(repo, cache)
        {

        }

        //tüm ürünleri getirme
        [HttpGet("GetOrder")]
        public dynamic GetOrder()
        {
            // throw new ApplicationException("test hata");

            List<Order> items;
            if (!cache.TryGetValue("GetOrder", out items))
            {
                items = repo.OrderRepository.FindAll().ToList<Order>();

                cache.Set("GetOrder", items, DateTimeOffset.UtcNow.AddSeconds(20));

                cache.Remove("GetOrder");
            }

            return new
            {
                sucess = true,
                data = items
            };
        }

        //kaydetme-güncelleme

        [HttpPost("Save")]

        public dynamic Save([FromBody] dynamic model)
        {
            dynamic json = JObject.Parse(model.GetRawText());

            Order item = new Order()
            {
                Id = json.Id,
              OrderNo= json.OrderNo,
              OrderDate = json.OrderDate,
              UserId = json.UserId,
              ProductId = json.ProductId,
              OrderAmount = json.Amount,
              BillId = json.BillId,
              CargoNo = json.CargoNo,
            };
            if (item.Id > 0)
                repo.OrderRepository.Update(item);
            else
                repo.OrderRepository.Create(item);
            repo.SaveChanges();
            cache.Remove("Order");
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
            repo.OrderRepository.RemoveOrder(id);
            return new
            {
                success = true,
                message = "Deleted"
            };
        }

        //orderno ya göre listeleme
        [HttpGet("GetOrderByOrderNo")]
        public dynamic GetOrderByOrderNo(int orderNo)
        {
            Order item;
            if (!cache.TryGetValue($"GetOrderByOrderNo_{orderNo}", out item))
            {
                item = repo.OrderRepository.FindByCondition(o => o.OrderNo == orderNo).FirstOrDefault();

                cache.Set($"GetOrderByOrderNo_{orderNo}", item, DateTimeOffset.UtcNow.AddSeconds(20));

                cache.Remove($"GetOrderByOrderNo_{orderNo}");
            }

            if (item != null)
            {
                return new
                {
                    success = true,
                    data = item
                };
            }
            else
            {
                return new
                {
                    success = false,
                    message = "Order not found"
                };
            }
        }

        //userId ye göre order listeleme

        [HttpGet("GetOrdersByUserId")]
        public dynamic GetOrdersByUserId(int userId)
        {
            List<Order> items;
            if (!cache.TryGetValue($"GetOrdersByUserId_{userId}", out items))
            {
                items = repo.OrderRepository.FindByCondition(o => o.UserId == userId).ToList();

                cache.Set($"GetOrdersByUserId_{userId}", items, DateTimeOffset.UtcNow.AddSeconds(20));

                cache.Remove($"GetOrdersByUserId_{userId}");
            }

            return new
            {
                success = true,
                data = items
            };
        }
    }
}
