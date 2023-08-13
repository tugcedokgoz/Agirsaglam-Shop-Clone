using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgirSaglam.Model.View
{
    [Table("V_CategoryAdminList")]
    public class V_CategoryAdminList
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string? ParentCategoryName { get; set; }
        public bool Status { get; set; }
    }
}
