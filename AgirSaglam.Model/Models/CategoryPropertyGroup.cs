﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgirSaglam.Model.Models
{
    [Table("tblCategoryPropertyGroup")]
    public class CategoryPropertyGroup
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int GroupId { get; set; }
        public Category Category { get; set; }
        public PropertyGroup Group { get; set; }

    }
}
