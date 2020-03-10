using System;
using System.Collections.Generic;
using System.Linq;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class PacientesRepository : IPacientesRepository
    {
        private readonly DataDbContext _context;
        public PacientesRepository (DataDbContext ctx)
        {
            _context = ctx;
        }
        public void Add(Pacientes pacientes)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    
                     var Log = new Log(){
                        Usuario_Logado = "Pacientes",
                        Acao = "Inclusão",
                        Rotina = "01Usu",
                        Data_Hora = DateTime.Now
                        };
                    _context.pacientes.Add(pacientes);
                    _context.log.Add(Log);
                    _context.SaveChanges();
                    transaction.Commit();
                }
                 catch (Exception e) 
                {
                    Console.WriteLine("Erro");
                    Console.WriteLine(e);
                    transaction.Rollback();
                }
            }
        }

        public Pacientes Find(int id)
        {
            return _context.pacientes
            .Include(e => e.endereco)
            .FirstOrDefault(u => u.Id == id);
        }

        public IEnumerable<Pacientes> GetAll()
        {
            return _context.pacientes
            .Include(e => e.endereco)
            .AsNoTracking()
            .ToList();
        }

        public void Remove(int id)
        {
           using (var transaction = _context.Database.BeginTransaction())
            {
                try 
                {
                    var pacientes = _context.pacientes.First(u => u.Id == id);
                    _context.pacientes.Remove(pacientes);
                    var Log = new Log(){
                        Usuario_Logado = "Pacientes",
                        Acao = "Exclusão",
                        Rotina = "03Usu",
                        Data_Hora = DateTime.Now
                        };
                    _context.log.Add(Log);
                    _context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception e) 
                {
                    Console.WriteLine("Erro");
                    Console.WriteLine(e);
                    transaction.Rollback();
                    return;
                }
            }
        }

        public void Update(Pacientes pacientes)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                   
                    var Log = new Log(){
                        Usuario_Logado = "Pacientes",
                        Acao = "Atualização",
                        Rotina = "03Usu",
                        Data_Hora = DateTime.Now
                        };
                    _context.pacientes.Update(pacientes);
                    _context.log.Add(Log);
                    _context.SaveChanges();
                    transaction.Commit();
                }
               
                catch (Exception e) 
                {
                    Console.WriteLine("Erro");
                    Console.WriteLine(e);
                    transaction.Rollback();
                    throw new System.Net.WebException (string.Format("Falha ao atualizar dados do Paciente"));
                }
            }     
        }

        public Usuario usuarioLogado(string email)
        {
            return _context.usuario
            .Where(x => x.Email.ToLower() == email.ToLower())
            .FirstOrDefault();
        }
    }
}