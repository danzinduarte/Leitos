using System;
using System.Collections.Generic;
using System.Linq;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class EnderecoRepository : IEnderecoRepository
    {
        private readonly DataDbContext _context;
        public EnderecoRepository (DataDbContext ctx)
        {
            _context = ctx;
        }
        public void Add(Endereco endereco)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.endereco.Add(endereco);
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

        public Endereco Find(int id)
        {
            return _context.endereco.FirstOrDefault(u => u.Id == id);
        }

        public IEnumerable<Endereco> GetAll()
        {
            return _context.endereco
            .AsNoTracking()
            .ToList();
        }

        public void Remove(int id)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try 
                {
                    var endereco = _context.endereco.First(u => u.Id == id);
                    _context.endereco.Remove(endereco);
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

        public void Update(Endereco endereco)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var Log = new Log(){
                    Usuario_Logado = "Teste de Atualização",
                    Acao = "Atualização de Endereço",
                    Rotina = "End03",
                    Data_Hora = DateTime.Now
                    };
                    _context.log.Add(Log);
                    _context.endereco.Update(endereco);
                    _context.SaveChanges();
                    transaction.Commit();
                }
               
                catch (Exception e) 
                {
                    Console.WriteLine("Erro");
                    Console.WriteLine(e);
                    transaction.Rollback();
                    throw new System.Net.WebException (string.Format("Falha ao atualizar dados do Endereço"));
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