using System;
using System.Collections.Generic;
using System.Linq;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class ProntuarioRepository : IProntuarioRepository
    {
        private readonly DataDbContext _context;
        public ProntuarioRepository (DataDbContext ctx)
        {
            _context = ctx;
        }

        public void Add(Prontuario prontuario)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try 
                {
                    _context.prontuario.Add(prontuario);
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

        public Prontuario Find(int id)
        {
            return _context.prontuario.FirstOrDefault(u => u.Id == id);
        }

        public IEnumerable<Prontuario> GetAll()
        {
            return _context.prontuario
            .AsNoTracking()
            .ToList();
        }

        public void Remove(int id)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try 
                {
                    var prontuario = _context.prontuario.First(u => u.Id == id);
                    _context.prontuario.Remove(prontuario);
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

        public void Update(Prontuario prontuario)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.prontuario.Update(prontuario);
                    _context.SaveChanges();
                    transaction.Commit();
                }
               
                catch (Exception e) 
                {
                    Console.WriteLine("Erro");
                    Console.WriteLine(e);
                    transaction.Rollback();
                    throw new System.Net.WebException (string.Format("Falha ao atualizar dados do Prontuario"));
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