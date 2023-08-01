using AgirSaglam.Model;
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
        public List<Order> GetOrdersByUserId(int userId)
        {
            return RepositoryContext.Orders.Where(o => o.UserId == userId).ToList();
        }
    }
}
