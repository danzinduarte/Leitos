using System.Collections.Generic;
using api.Models;

namespace api.Repository
{
    public interface ILogRepository
    {
        void Add (Log log);
        IEnumerable<Log> GetAll();
        Log Find (int id);
        Usuario usuarioLogado(string email);
    }
}