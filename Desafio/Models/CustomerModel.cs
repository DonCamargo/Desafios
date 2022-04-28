using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Desafio.Models
{
    [Table("Customer")]
    public class CustomerModel
    {
        [Key]
        public long Id { get; set; }
        [Required]
        [Column("Name")]
        public string Name { get; set; }
        [Column("Age")]
        public string Age { get; set; }
        [Column("Email")]
        public string Email { get; set; }
        [Required]
        [Column("Document")]
        public string Document { get; set; }
    }
}
