using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgirSaglam.Model.Models
{
    [Table("tblCategory")]
    public class Category
    {
        public Category()
        {
            ChildCategories=new HashSet<Category>();
        }
        public int Id { get; set; }
        public string Name{ get; set; }
        public int? ParentCategoryId { get; set; }
        public bool? Status { get; set; }
        public int? ProductId { get; set; }

        public virtual Category? ParentCategory { get; set; }
        public virtual ICollection<Category> ChildCategories { get; set; }
    }
}
