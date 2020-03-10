using System.Collections.Generic;
using api.Models;

namespace api.Repository
{
    public interface ILeitosRepository
    {
         void Add (Leitos leitos);
         IEnumerable<Leitos> GetAll();
         Leitos Find (int id);
         void Remove (int id);
         void Update (Leitos leitos);
         Usuario usuarioLogado(string email);
    }
}