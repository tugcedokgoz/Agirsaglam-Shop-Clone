using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgirSaglam.Model.View
{
    [Table("V_AktiveUsers")]
    public class V_AktiveUsers
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string? Email { get; set; }
        public DateTime? CreateDate { get; set; }
        public bool EmailConfirm { get; set; }

        //    public string UserName { get; set; }
        //    public string Name { get; set; }
        //    public DateTime CreateDate { get; set; }
        //    public bool EmailConfirm { get; set; }
    }
}
