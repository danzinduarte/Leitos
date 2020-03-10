using System;
using System.Collections.Generic;
using System.Linq;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class LeitosRepository : ILeitosRepository
    {
        private readonly DataDbContext _context;
        public LeitosRepository (DataDbContext ctx)
        {
            _context = ctx;
        }
        public void Add(Leitos leitos)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var Log = new Log()
                    {
                        Usuario_Logado = "Leitos",
                        Acao = "Inclusão",
                        Rotina = "01Usu",
                        Data_Hora = DateTime.Now
                    };
                    _context.log.Add(Log);
                    _context.leitos.Add(leitos);
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

        public Leitos Find(int id)
        {
            return _context.leitos.FirstOrDefault(u => u.Id == id);
        }

        public IEnumerable<Leitos> GetAll()
        {
            return _context.leitos
            .AsNoTracking()
            .ToList();
        }

        public void Remove(int id)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try 
                {
                    var salvaLog = new Log()
                    {
                        Usuario_Logado = "TESTE DO CONTROLLER",
                        Acao = "TESTE DELETE",
                        Rotina = "01Usu",
                        Data_Hora = DateTime.Now 
                    };
                   _context.log.Add(salvaLog);
                    var leitos = _context.leitos.First(u => u.Id == id);
                    _context.leitos.Remove(leitos);
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

        public void Update(Leitos leitos)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var salvaLog = new Log()
                    {
                        Usuario_Logado = "TESTE DO CONTROLLER",
                        Acao = "TESTE DELETE",
                        Rotina = "01Usu",
                        Data_Hora = DateTime.Now 
                    };
                    _context.log.Add(salvaLog);  
                    _context.leitos.Update(leitos);
                    _context.SaveChanges();
                    transaction.Commit();
                }
               
                catch (Exception e) 
                {
                    Console.WriteLine("Erro");
                    Console.WriteLine(e);
                    transaction.Rollback();
                    throw new System.Net.WebException (string.Format("Falha ao atualizar dados do Leito"));
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