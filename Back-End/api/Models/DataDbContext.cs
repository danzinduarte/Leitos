
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Models
{
    
    public class DataDbContext : DbContext
    {
        public DataDbContext(DbContextOptions<DataDbContext> options) : base (options) 
        { }
        public DbSet<Usuario> usuario { get; set; }    
        public DbSet<Log> log {get; set;}   
        public DbSet<Leitos> leitos {get; set;}   
        public DbSet<Pacientes> pacientes {get; set;}   
        public DbSet<Setor> setor {get; set;} 
        public DbSet<Endereco> endereco {get; set;} 
        public DbSet<Prontuario> prontuario {get; set;} 
        public DbSet<Profissionais> profissionais {get; set;} 
        
      
    }
}