using System;
using System.Collections.Generic;
using System.Linq;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class ProfissionaisRepository : IProfissionaisRepository
    {
        private readonly DataDbContext _context;
        public ProfissionaisRepository (DataDbContext ctx)
        {
            _context = ctx;
        }

        public void Add(Profissionais profissionais)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try 
                {
                    var Log = new Log()
                    {
                        Usuario_Logado = "Post Profissionais",
                        Acao = "Inclusão de Profissional",
                        Rotina = "Post Profissional",
                        Data_Hora = DateTime.Now
                    };
                    _context.log.Add(Log);
                    _context.profissionais.Add(profissionais);
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

        public Profissionais Find(int id)
        {
            return _context.profissionais.FirstOrDefault(u => u.Id == id);
        }

        public IEnumerable<Profissionais> GetAll()
        {
            return _context.profissionais
            .AsNoTracking()
            .ToList();
        }

        public void Remove(int id)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try 
                {
                    var profissionais = _context.profissionais.First(u => u.Id == id);
                    _context.profissionais.Remove(profissionais);
                    var Log = new Log()
                    {
                        Usuario_Logado = "Delete Profissional",
                        Acao = "Delete de Profissional",
                        Rotina = "Delete Profissional",
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

        public void Update(Profissionais profissionais)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                   
                    var Log = new Log()
                    {
                        Usuario_Logado = "Update Profissional",
                        Acao = "Atualização Profissional",
                        Rotina = "Put Profissionais",
                        Data_Hora = DateTime.Now 
                    };
                    _context.log.Add(Log);  
                    _context.profissionais.Update(profissionais);
                    _context.SaveChanges();
                    transaction.Commit();
                }
               
                catch (Exception e) 
                {
                    Console.WriteLine("Erro");
                    Console.WriteLine(e);
                    transaction.Rollback();
                    throw new System.Net.WebException (string.Format("Falha ao atualizar dados do Profissional"));
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