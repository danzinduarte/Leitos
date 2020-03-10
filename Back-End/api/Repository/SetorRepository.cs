using System;
using System.Collections.Generic;
using System.Linq;
using api.Models;
using Microsoft.EntityFrameworkCore;


namespace api.Repository
{
    public class SetorRepository : ISetorRepository
    {
        private readonly DataDbContext _context;
        public SetorRepository(DataDbContext ctx)
        {
            _context = ctx;
        }
        public void Add(Setor setor)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try 
                {
                    var Log = new Log(){
                        Usuario_Logado = "Post Setor",
                        Acao = "Inclusão de Setor",
                        Rotina = "Setor Post",
                        Data_Hora = DateTime.Now
                        };
                    _context.log.Add(Log);
                    _context.setor.Add(setor);
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

        public Setor Find(int id)
        {
            return _context.setor.FirstOrDefault(u => u.Id == id);
        }

        public IEnumerable<Setor> GetAll()
        {
             return _context.setor
             .AsNoTracking()
            .ToList();
        }

        public void Remove(int id)
        {
            var transaction = _context.Database.BeginTransaction();
            try {
                
                var setor = _context.setor.First(u => u.Id == id);
                _context.setor.Remove(setor);
                  var salvaLog = new Log(){
                    Usuario_Logado = "Delete Setor",
                    Acao = "Exclusão Setor",
                    Rotina = "Delete Setor",
                    Data_Hora = DateTime.Now 
                };
                _context.log.Add(salvaLog);
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

        public void Update(Setor setor)
        {
           using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var Log = new Log(){
                    Usuario_Logado = "Update Setor",
                    Acao = "Atualização Setor",
                    Rotina = "Put Setor",
                    Data_Hora = DateTime.Now
                    };
                    _context.log.Add(Log);
                    _context.setor.Update(setor);
                    _context.SaveChanges();
                    transaction.Commit();
                }
               
                catch (Exception e) 
                {
                    Console.WriteLine("Erro");
                    Console.WriteLine(e);
                    transaction.Rollback();
                    throw new System.Net.WebException (string.Format("Falha ao atualizar dados do Setor"));
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