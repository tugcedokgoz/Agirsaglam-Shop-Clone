using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgirSaglam.Model
{
    [Table("tblBill")]
    public class Bill
    {
        public int Id { get; set; }
        public int AdressId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string TcNo { get; set; }

    }
}
