using System;
using api.Models;
using api.Repository;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[Controller]")]
    public class ProfissionaisController : Controller
    {
        private readonly IProfissionaisRepository _profissionaisRepository;
        public ProfissionaisController(IProfissionaisRepository profissionaisRepo)
        {
            _profissionaisRepository = profissionaisRepo;
        }

        [HttpGet]
        public ActionResult<RetornoView<Profissionais>> GetAll()
        {
            return Ok (new{data = _profissionaisRepository.GetAll()});
        }  
        
        [HttpGet("{id}", Name = "GetProfissionais")]
        public ActionResult<RetornoView<Profissionais>> GetById(int id)
        {
            
            var profissionais = _profissionaisRepository.Find(id);
            if(profissionais==null)
            {
                return NotFound();           
            }
            return Ok( new {data = profissionais});
        }
        [HttpPost]
        public ActionResult<RetornoView<Profissionais>> Create ([FromBody]Profissionais profissionais)
        {
            //var usuarioLogado = _profissionaisRepository.usuarioLogado(User.Identity.Name);
            try
            {
                
                _profissionaisRepository.Add(profissionais);
 
            }
            catch (Exception ex) 
            {
                var result = new RetornoView<Profissionais>() { sucesso = false, erro = ex.Message };
                return BadRequest(result);
            }

            var resultado = new RetornoView<Profissionais>() { data = profissionais, sucesso = true };
            return CreatedAtRoute("GetProfissionais", new { id = profissionais.Id}, resultado);  
            
            
        }
        [HttpPut("{id}")]
        public ActionResult<RetornoView<Profissionais>> Update(int id, [FromBody] Profissionais profissionais)
        {
            var _profissionais = _profissionaisRepository.Find(id);
            if(_profissionais == null)
            {
                return NotFound();
            }        
            try 
            {
                _profissionais.Cargo                    =   profissionais.Cargo;
                _profissionais.Nome_Profissionais       =   profissionais.Nome_Profissionais;
                _profissionaisRepository.Update(_profissionais);
            }
             catch (Exception ex)
            {
                var result = new RetornoView<Profissionais>() { sucesso = false, erro = ex.Message };
                return BadRequest(result);
            }

            var resultado = new RetornoView<Profissionais>() { data = _profissionais, sucesso = true };
            return resultado;
            
        }
        [HttpDelete("{id}")]
        public ActionResult<RetornoView<Profissionais>> Delete(int id) 
        {
            var profissionais  = _profissionaisRepository.Find(id);
            if (profissionais == null) 
            {
                return NotFound();
            }
                
                _profissionaisRepository.Remove(id);
                
                    
            if (_profissionaisRepository.Find(id) == null) 
            {
                var resultado = new RetornoView<Profissionais>() { sucesso = true };
                return resultado;
                
            }
            else 
            {
                var resultado = new RetornoView<Profissionais>() { sucesso = false };
                return BadRequest(resultado);
            }
        }
    }
}