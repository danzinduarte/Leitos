using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using api.Models;

namespace api.Models
{
    [Table("Usuario")]
    public class Usuario
    {
        [Key]
        public int Id {get; set; }
        public string Nome {get; set; }
        public string Senha {get; set; }
        public string Email {get; set; }
        public string Token {get; set; }
        public bool Tipo { get; set; }
    }
}