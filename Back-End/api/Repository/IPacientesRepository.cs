using System.Collections.Generic;
using api.Models;

namespace api.Repository
{
     public interface IPacientesRepository
    {
        void Add (Pacientes paciente);
        IEnumerable<Pacientes> GetAll();
        Pacientes Find (int id);
        void Remove (int id);
        void Update (Pacientes pacientes);
        Usuario usuarioLogado(string email);
    }

}