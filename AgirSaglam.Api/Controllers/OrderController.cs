﻿using AgirSaglam.Model.Models;
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
    public class OrderController : BaseController
    {
        public OrderController(RepositoryWrapper repo, IMemoryCache cache) : base(repo, cache)
        {

        }

        //tüm ürünleri getirme
        [HttpGet("GetOrder")]
        public dynamic GetOrder()
        {
            var items = cache.GetOrCreate("GetOrder", entry =>
            {
                entry.SetAbsoluteExpiration(TimeSpan.FromSeconds(20));
                return repo.OrderRepository.FindAll()
                    .Include(c => c.User)
                    .Include(c => c.Product)
                    .Include(c => c.Bill)
                    .Select(c => new
                    {
                        c.Id,
                        c.UserId,
                        c.ProductId,
                        c.BillId,
                        c.OrderNo,
                        c.OrderDate,
                        c.OrderAmount,
                        c.CargoNo,
                        User = new
                        {
                            c.User.Id,
                            c.User.UserName,
                        },
                        Product = new
                        {
                            c.Product.Id,
                            c.Product.Name,
                            c.Product.Price,
                        },
                        Bill = new
                        {
                            c.Bill.Id,
                            c.Bill.Name,
                            c.Bill.Surname,
                            c.Bill.Adress,
                            c.Bill.Adress.City,
                            c.Bill.Adress.District,
                            c.Bill.Adress.PostCode,
                            c.Bill.Email,
                            c.Bill.PhoneNo,
                            c.Bill.TcNo,

                        }
                    })
                    .ToList();
            });

            return new
            {
                success = true,
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
                item = repo.OrderRepository.FindByCondition(o => o.OrderNo == orderNo)
                    .Include(o => o.User)   
                    .Include(o => o.Product) 
                    .Include(o => o.Bill)   
                    .FirstOrDefault();

                cache.Set($"GetOrderByOrderNo_{orderNo}", item, DateTimeOffset.UtcNow.AddSeconds(20));
            }

            if (item != null)
            {
                return new
                {
                    success = true,
                    data = new
                    {
                        item.Id,
                        item.OrderNo,
                        item.OrderDate,
                        item.OrderAmount,
                        item.CargoNo,
                        item.UserId,
                        item.ProductId,
                        item.BillId,
           
                        User = new
                        {
                            item.User.Id,
                            item.User.UserName,
                          
                        },
                        Product = new
                        {
                            item.Product.Id,
                            item.Product.Name,
                           
                        },
                        Bill = new
                        {
                            item.Bill.Id,
                            item.Bill.Name,
                            item.Bill.Surname,
                         
                        }
                    }
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
            Order item;
            if (!cache.TryGetValue($"GetOrdersByUserId{userId}", out item))
            {
                item = repo.OrderRepository.FindByCondition(o => o.UserId == userId)
                    .Include(o => o.User)   
                    .Include(o => o.Product) 
                    .Include(o => o.Bill)   
                    .FirstOrDefault();

                cache.Set($"GetOrdersByUserId{userId}", item, DateTimeOffset.UtcNow.AddSeconds(20));
            }

            if (item != null)
            {
                return new
                {
                    success = true,
                    data = new
                    {
                        item.Id,
                        item.OrderNo,
                        item.UserId,
                        item.ProductId,
                        item.BillId,
                   
                        User = new
                        {
                            item.User.Id,
                            item.User.UserName,
                     
                        },
                        Product = new
                        {
                            item.Product.Id,
                            item.Product.Name,
                     
                        },
                        Bill = new
                        {
                            item.Bill.Id,
                            item.Bill.Name,
                            item.Bill.Surname,
                        
                        }
                    }
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



    }
}
