using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgirSaglam.Model
{
    [Table("tblProduct")]
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public int Amount { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<ProductCategory> ProductCategories { get; set; }
    }
}
