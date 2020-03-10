using System.Collections.Generic;
using System.Linq;
using api.Models;


namespace api.Repository
{
    public interface IUsuarioRepository
    {
         void Add (Usuario usuario);
         IEnumerable<Usuario> GetAll();
         Usuario Find (int id);
         void Remove (int id);
         void Update (Usuario usuario);
         Usuario usuarioLogado(string email);
         
    }

}