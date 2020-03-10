using System.Collections.Generic;
using api.Models;

namespace api.Repository
{
    public interface ISetorRepository
    {
        void Add (Setor setor);
        IEnumerable<Setor> GetAll();
        Setor Find (int id);
        void Remove (int id);
        void Update (Setor setor);
        Usuario usuarioLogado(string email);
        
    }
}