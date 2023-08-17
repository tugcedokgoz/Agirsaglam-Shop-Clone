using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgirSaglam.Model.View
{
    [Table("V_ProductAdminList")]
    public class V_ProductAdminList
    {
        [Key]
        public int ProductId { get; set; }

        public int CategoryId { get; set; }

        public string ProductName { get; set; }

        public string CategoryName { get; set; }

        public decimal Price { get; set; }

        public decimal? DiscountPrice { get; set; }

        public int Amount { get; set; }

        public string? Description { get; set; }

        public string? Image { get; set; }
    }
}
