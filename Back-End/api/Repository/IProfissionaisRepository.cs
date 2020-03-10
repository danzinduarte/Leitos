using System.Collections.Generic;
using api.Models;

namespace api.Repository
{
    public interface IProfissionaisRepository
    {
        void Add (Profissionais profissionais);
        IEnumerable<Profissionais> GetAll();
        Profissionais Find (int id);
        void Remove (int id);
        void Update (Profissionais profissionais);
        Usuario usuarioLogado(string email);
    }
}