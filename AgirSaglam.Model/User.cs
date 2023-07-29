using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgirSaglam.Model
{
    [Table("tblUser")]
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool EmailConfirm { get; set; }
        public DateTime EmailConfirmDate { get; set; }
        public bool Status { get; set; }
        public int RoleId { get; set; }
        public int AdressId { get; set; }
    }
}
