using System;
using System.Collections.Generic;
using System.Linq;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class LogRepository : ILogRepository
    {
        private readonly DataDbContext _context;
        public LogRepository (DataDbContext ctx)
        {
            _context = ctx;
        }
        public void Add(Log log)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try 
                {
                    _context.log.Add(log);
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

        public Log Find(int id)
        {
            return _context.log.FirstOrDefault(u => u.Id == id);
        }

        public IEnumerable<Log> GetAll()
        {
            return _context.log
            .AsNoTracking()
            .ToList();
        }
        public Usuario usuarioLogado(string email)
        {
            return _context.usuario
            .Where(x => x.Email.ToLower() == email.ToLower())
            .FirstOrDefault();
        }
    }
}