using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using api.Models;

namespace api.Models
{
    [Table("Leitos")]
    public class Leitos
    {
        [Key]
        public int Id {get; set; }
        public bool Ocupado {get; set; }
        public DateTime Data_Entrada {get; set; }
        public DateTime Data_Saida {get; set; }
        [ForeignKey("setor")]
        public int Setor_Id {get; set;}
        public virtual Setor setor { get; set; }
        
        [ForeignKey("pacientes")]
        public int Paciente_Id {get; set;}
        public virtual Pacientes pacientes { get; set; }
    }
}