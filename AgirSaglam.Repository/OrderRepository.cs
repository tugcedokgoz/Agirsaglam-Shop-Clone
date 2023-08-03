﻿using AgirSaglam.Model;
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
                .Include(c => c.User)    // User ilişkisel verisini yükleme
                .Include(c => c.Product) // Product ilişkisel verisini yükleme
                .Include(c => c.Bill) // Bill ilişkisel verisini yükleme
                .ToListAsync();

            return comments;
        }
        //orderno ya göre sipariş getirme
        public async Task<List<Order>> GetOrderByOrderNo(int orderNo)
        {
            var comments = await RepositoryContext.Orders
                .Where(c => c.OrderNo == orderNo)
                .Include(c => c.User)    // User ilişkisel verisini yükleme
                .Include(c => c.Product) // Product ilişkisel verisini yükleme
                .Include(c => c.Bill) // Bill ilişkisel verisini yükleme
                .ToListAsync();

            return comments;
        }
    }
}
