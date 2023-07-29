using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgirSaglam.Model
{
    [Table("tblProductProperty")]
    public class ProductProperty
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int PropertyId { get; set; }
    }
}
