using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgirSaglam.Model.Models
{
    [Table("tblAdress")]
    public class Adress
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string PostCode { get; set; }
    }
}
