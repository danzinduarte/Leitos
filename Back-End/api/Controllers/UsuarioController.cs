using api.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using api.Models;



namespace api.Controllers
{
    [Route("api/[Controller]")]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public UsuarioController(IUsuarioRepository usuarioRepo)
        {
            _usuarioRepository = usuarioRepo;
        }

        [HttpGet]
        public ActionResult<RetornoView<Usuario>> GetAll()
        {
            return Ok (new{data = _usuarioRepository.GetAll()});
        }  
        
        [HttpGet("{id}", Name = "GetUsuario")]
        public ActionResult<RetornoView<Usuario>> GetById(int id)
        {
            
            var usuario = _usuarioRepository.Find(id);
            if(usuario==null)
            {
                return NotFound();           
            }
            return Ok( new {data = usuario});
        }
        [HttpPost]
        public ActionResult<RetornoView<Usuario>> Create ([FromBody]Usuario usuario)
        {
            //var usuarioLogado = _usuarioRepository.usuarioLogado(User.Identity.Name);
            try
            {
                usuario.Senha = TrataHash.GeraMD5Hash(usuario.Senha);
                _usuarioRepository.Add(usuario);
            }
            catch (Exception ex) 
            {
                var result = new RetornoView<Usuario>() { sucesso = false, erro = ex.Message };
                return BadRequest(result);
            }

            var resultado = new RetornoView<Usuario>() { data = usuario, sucesso = true };
            return CreatedAtRoute("GetUsuario", new { id = usuario.Id}, resultado);  
            
            
        }
        [HttpPut("{id}")]
        public ActionResult<RetornoView<Usuario>> Update(int id, [FromBody] Usuario usuario)
        {
            var _usuario = _usuarioRepository.Find(id);
            if(_usuario == null)
            {
                return NotFound();
            }        
            try 
            {
               
                _usuario.Nome   = usuario.Nome;
                _usuario.Senha  = usuario.Senha;
                _usuario.Email  = usuario.Email; 
                
                _usuarioRepository.Update(_usuario);
            }
             catch (Exception ex)
            {
                var result = new RetornoView<Usuario>() { sucesso = false, erro = ex.Message };
                return BadRequest(result);
            }

            var resultado = new RetornoView<Usuario>() { data = _usuario, sucesso = true };
            return resultado;
            
        }
        [HttpDelete("{id}")]
        public ActionResult<RetornoView<Usuario>> Delete(int id) 
        {
            var usuario  = _usuarioRepository.Find(id);
            if (usuario == null) 
            {
                return NotFound();
            }
                
                _usuarioRepository.Remove(id);
                    
            if (_usuarioRepository.Find(id) == null) 
            {
                var resultado = new RetornoView<Usuario>() { sucesso = true };
                return resultado;
                
            }
            else 
            {
                var resultado = new RetornoView<Usuario>() { sucesso = false };
                return BadRequest(resultado);
            }
        }
    }
}