using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    [Table("Endereco")]
    public class Endereco
    {
        [Key]
        public int Id {get; set;}
        public string Rua {get; set;}
        public string Numero {get; set;}
        public string Complemento {get; set;}
        public string Bairro {get; set;}
        public string Cidade {get; set;}
        public string Estado {get; set;}
        public string Uf {get; set;}

    }
}