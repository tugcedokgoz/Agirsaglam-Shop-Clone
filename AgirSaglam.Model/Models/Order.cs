using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgirSaglam.Model.Models
{
    [Table("tblOrder")]
    public class Order
    {
        public int Id { get; set; }
        public int OrderNo { get; set; }
        public DateTime OrderDate { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int OrderAmount { get; set; }
        public int BillId { get; set; }
        public Bill Bill { get; set; }
        public string CargoNo { get; set; }
    }
}
