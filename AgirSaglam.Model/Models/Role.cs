﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgirSaglam.Model.Models
{
    [Table("tblRole")]
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
