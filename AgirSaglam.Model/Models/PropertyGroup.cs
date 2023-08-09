using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgirSaglam.Model.Models
{
    [Table("tblPropertyGroup")]
    public class PropertyGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
