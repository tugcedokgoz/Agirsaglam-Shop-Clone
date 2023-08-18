using AgirSaglam.Model.Models;
using AgirSaglam.Model.View;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgirSaglam.Repository
{
    public class OrderRepository : RepositoryBase<Order>
    {
        public OrderRepository(RepositoryContext context) : base(context)
        {

        }
        //silme
        public void RemoveOrder(int orderId)
        {
            RepositoryContext.Orders.Where(r => r.Id == orderId).ExecuteDelete();
        }
        // UserId'ye göre siparişleri getirme
        public async Task<List<Order>> GetOrderByUserId(int userId)
        {
            var comments = await RepositoryContext.Orders
                .Where(c => c.UserId == userId)
                .Include(c => c.User)   
                .Include(c => c.Product) 
                .Include(c => c.Bill) 
                .ToListAsync();

            return comments;
        }
        //orderno ya göre sipariş getirme
        public async Task<List<Order>> GetOrderByOrderNo(int orderNo)
        {
            var comments = await RepositoryContext.Orders
                .Where(c => c.OrderNo == orderNo)
                .Include(c => c.User)    
                .Include(c => c.Product) 
                .Include(c => c.Bill) 
                .ToListAsync();

            return comments;
        }

        

    }
}
