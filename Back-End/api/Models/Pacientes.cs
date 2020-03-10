using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using api.Models;

namespace api.Models
{
    [Table("Pacientes")]
    public class Pacientes
    {
        [Key]
        public int Id {get; set; }
        public string Nome {get; set; }
        public DateTime Data_Internacao {get; set;}
        public string Acompanhante {get; set; }
        public string Telefone {get; set; }    
        
        [ForeignKey("endereco")]
        public int Endereco_Id {get; set;}
        public virtual Endereco endereco { get; set; }
    }
}