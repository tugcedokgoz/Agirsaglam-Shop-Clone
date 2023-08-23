using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgirSaglam.Model.Models
{
    [Table("tblContact")]
    public class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Text { get; set; }
        public bool? AnswerStatus { get; set; }
        public int? AnswerUserId { get; set; }
        public DateTime? TextDate { get; set; }
    }
}
