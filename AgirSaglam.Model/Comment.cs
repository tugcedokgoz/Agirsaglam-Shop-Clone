using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgirSaglam.Model
{
    [Table("tblComment")]
    public class Comment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string Explanation { get; set; }
        public DateTime Date { get; set; }
        public decimal Point { get; set; }
        public string? Answer { get; set; }
        public bool? Status { get; set; }
        public DateTime? StatusDate { get; set; }
        public int? ConfirmUserId { get; set; }
    }
}
