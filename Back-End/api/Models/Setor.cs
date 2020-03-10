using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using api.Models;

namespace api.Models
{
    [Table("Setor")]
    public class Setor
    {
    [Key]
       public int Id {get; set;}
       public string Nome_Setor{get; set;}        
       public string Numero_Leito {get; set;}
    }
}