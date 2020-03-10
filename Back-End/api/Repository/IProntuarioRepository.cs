using System.Collections.Generic;
using api.Models;

namespace api.Repository
{
    public interface IProntuarioRepository
    {
        void Add (Prontuario prontuario);
        IEnumerable<Prontuario> GetAll();
        Prontuario Find (int id);
        void Remove (int id);
        void Update (Prontuario prontuario);
        Usuario usuarioLogado(string email);
    }
}