using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    [Table("Prontuario")]
    public class Prontuario
    {
        [Key]
        public int Id {get; set;}
        public string Medicacao {get; set;}
        public string Dosagem {get; set;}
        public DateTime Hora_Medicamento {get; set;}
        public string Estado_Paciente {get; set;} 
          
        [ForeignKey("log")]
        public int LogProntuarios_Id {get; set;}
        public virtual Log log { get; set; }

        [ForeignKey("profissionais")]
        public int Profissionais_Id {get; set;}
        public virtual Profissionais profissionais { get; set; }     
    }
}