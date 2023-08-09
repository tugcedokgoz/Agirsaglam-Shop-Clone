using AgirSaglam.Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgirSaglam.Repository
{
    public class BillRepository:RepositoryBase<Bill>
    {
        public BillRepository(RepositoryContext context):base(context)
        {
            
        }
        public void RemoveBill(int billId)
        {
            RepositoryContext.Users.Where(r => r.Id == billId).ExecuteDelete();
        }
        //kullaniciId yer göre bill getirme
        public async Task<List<Bill>> GetBillsByUserId(int userId)
        {
            var bills = await RepositoryContext.Bills
                .Where(b => b.UserId == userId)
                .ToListAsync();

            return bills;
        }
    }
}
