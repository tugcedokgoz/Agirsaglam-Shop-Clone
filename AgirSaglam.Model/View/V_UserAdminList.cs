﻿using AgirSaglam.Model.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace AgirSaglam.Model.View
{
    [Table("V_UserAdminList")]
    public class V_UserAdminList
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int AdressId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool EmailConfirm { get; set; }
        public DateTime? EmailConfirmDate { get; set; }
        public int Status { get; set; }
        public string RoleName { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string PostCode { get; set; }
        public Role Role { get; set; }
        public Adress Adress { get; set; }

    }
}
