using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgirSaglam.Model.Models
{
    [Table("tblProperty")]
    public class Property
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int GroupId { get; set; }
        public PropertyGroup Group { get; set; }
    }
}
