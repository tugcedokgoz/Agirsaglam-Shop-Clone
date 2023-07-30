using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgirSaglam.Model
{
    [Table("tblCategory")]
    public class Category
    {
        public Category()
        {
            ChildCategories = new HashSet<Category>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentCategoryId { get; set; }
        public bool? Status { get; set; }

        public virtual Category? ParentCategory { get; set; } //kategorinin bir üst kategorisi olabilir oda null olabilir
        public virtual ICollection<Category> ChildCategories { get; set; } // bu kategorinin birden fazla alt kategorisi olabilir
    }
}
