using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    [Table("Profissionais")]
    public class Profissionais
    {
        [Key]
        public int Id {get; set;}
        public string Nome_Profissionais {get; set;}
        public string Cargo {get; set;}
       
    }
}