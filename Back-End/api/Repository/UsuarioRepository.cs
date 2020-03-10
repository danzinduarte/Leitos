using System;
using System.Collections.Generic;
using System.Linq;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly DataDbContext _context;
        
        public UsuarioRepository(DataDbContext ctx)
        {
            _context = ctx;
        }
        public void Add(Usuario usuario)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try 
                {
                    var Log = new Log(){
                    Usuario_Logado = "Post Usuario",
                    Acao = "Inclusão de Usuario",
                    Rotina = "Post Usuario",
                    Data_Hora = DateTime.Now
                    };
                    _context.log.Add(Log);
                    _context.usuario.Add(usuario);
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

        public Usuario Find(int id)
        {
            return _context.usuario.FirstOrDefault(u => u.Id == id);
        }

        public IEnumerable<Usuario> GetAll()
        {
             return _context.usuario
             .AsNoTracking()
            .ToList();
        }

        public void Remove(int id)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try 
                {
                    var usuario = _context.usuario.First(u => u.Id == id);
                    _context.usuario.Remove(usuario);
                    var Log = new Log(){
                        Usuario_Logado = "Teste Delete",
                        Acao = "Exclusão de Usuario",
                        Rotina = "Delete Usuario",
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

        public void Update(Usuario usuario)
        {
           using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var Log = new Log()
                    {
                        Usuario_Logado = "Update Usuario",
                        Acao = "Atualização de Usuario",
                        Rotina = "Put Usuario",
                        Data_Hora = DateTime.Now
                    };
                    _context.log.Add(Log);
                    _context.usuario.Update(usuario);
                    _context.SaveChanges();
                    transaction.Commit();
                }
               
                catch (Exception e) 
                {
                    Console.WriteLine("Erro");
                    Console.WriteLine(e);
                    transaction.Rollback();
                    throw new System.Net.WebException (string.Format("Falha ao atualizar dados do Usuario"));
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