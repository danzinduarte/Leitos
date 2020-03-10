using System;
using api.Models;
using api.Repository;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[Controller]")]
    public class ProntuarioController : Controller
    {
        private readonly IProntuarioRepository _prontuarioRepository;
        public ProntuarioController(IProntuarioRepository prontuarioRepo)
        {
            _prontuarioRepository = prontuarioRepo;
        }

        [HttpGet]
        public ActionResult<RetornoView<Prontuario>> GetAll()
        {
            return Ok (new{data = _prontuarioRepository.GetAll()});
        }  
        
        [HttpGet("{id}", Name = "GetProntuario")]
        public ActionResult<RetornoView<Prontuario>> GetById(int id)
        {
            
            var prontuario = _prontuarioRepository.Find(id);
            if(prontuario==null)
            {
                return NotFound();           
            }
            return Ok( new {data = prontuario});
        }
        [HttpPost]
        public ActionResult<RetornoView<Prontuario>> Create ([FromBody]Prontuario prontuario)
        {
            var usuarioLogado = _prontuarioRepository.usuarioLogado(User.Identity.Name);
            try
            {
                var Log = new Log(){
                Usuario_Logado = usuarioLogado.Nome,
                Acao = "Inclusão",
                Rotina = "01Usu",
                Data_Hora = DateTime.Now
                };
                prontuario.log = Log;
                _prontuarioRepository.Add(prontuario);
 
            }
            catch (Exception ex) 
            {
                var result = new RetornoView<Prontuario>() { sucesso = false, erro = ex.Message };
                return BadRequest(result);
            }

            var resultado = new RetornoView<Prontuario>() { data = prontuario, sucesso = true };
            return CreatedAtRoute("GetProntuario", new { id = prontuario.Id}, resultado);  
            
            
        }
        [HttpPut("{id}")]
        public ActionResult<RetornoView<Prontuario>> Update(int id, [FromBody] Prontuario prontuario)
        {
            var _prontuario = _prontuarioRepository.Find(id);
            if(_prontuario == null)
            {
                return NotFound();
            }        
            try 
            {
               
               
               var salvaLog = new Log(){
                    Usuario_Logado = "TESTE DO CONTROLLER",
                    Acao = "TESTE DELETE",
                    Rotina = "Usuarios",
                    Data_Hora = DateTime.Now 
                };
                prontuario.log = salvaLog;  
                _prontuarioRepository.Update(prontuario);
            }
             catch (Exception ex)
            {
                var result = new RetornoView<Prontuario>() { sucesso = false, erro = ex.Message };
                return BadRequest(result);
            }

            var resultado = new RetornoView<Prontuario>() { data = _prontuario, sucesso = true };
            return resultado;
            
        }
        [HttpDelete("{id}")]
        public ActionResult<RetornoView<Prontuario>> Delete(int id) 
        {
            var prontuario  = _prontuarioRepository.Find(id);
            if (prontuario == null) 
            {
                return NotFound();
            }
                var salvaLog = new Log(){
                    Usuario_Logado = "TESTE DO CONTROLLER",
                    Acao = "TESTE DELETE",
                    Rotina = "01Usu",
                    Data_Hora = DateTime.Now 
                };
                prontuario.log = salvaLog;
                _prontuarioRepository.Remove(id);
                
                    
            if (_prontuarioRepository.Find(id) == null) 
            {
                var resultado = new RetornoView<Prontuario>() { sucesso = true };
                return resultado;
                
            }
            else 
            {
                var resultado = new RetornoView<Prontuario>() { sucesso = false };
                return BadRequest(resultado);
            }
        }
    }
}