using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using api.Models;

namespace api.Models
{
    [Table("Log")]
    public class Log
    {
    [Key]
       public int Id {get; set;}
       public string Usuario_Logado {get; set;}        
       public string Rotina { get; set;}
       public string Acao {get; set;}
       public DateTime Data_Hora {get; set;}
      
    }
}