using System;
using api.Models;
using api.Repository;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers

{
    [Route("api/[Controller]")]
    public class SetorController : Controller
    {
      private readonly ISetorRepository _setorRepository;
        public SetorController(ISetorRepository setorRepo)
        {
            _setorRepository = setorRepo;
        }  
         [HttpGet]
        public ActionResult<RetornoView<Setor>> GetAll()
        {
            return Ok (new{data = _setorRepository.GetAll()});
        }  
        
        [HttpGet("{id}", Name = "GetSetor")]
        public ActionResult<RetornoView<Setor>> GetById(int id)
        {
            
            var setor = _setorRepository.Find(id);
            if(setor == null)
            {
                return NotFound();           
            }
            return Ok( new {data = setor});
        }
        [HttpPost]
        public ActionResult<RetornoView<Setor>> Create ([FromBody]Setor setor)
        {
            //var usuarioLogado = _setorRepository.usuarioLogado(User.Identity.Name);
            try
            {
                _setorRepository.Add(setor);
            }
            catch (Exception ex) 
            {
                var result = new RetornoView<Setor>() { sucesso = false, erro = ex.Message };
                return BadRequest(result);
            }

            var resultado = new RetornoView<Setor>() { data = setor, sucesso = true };
            return CreatedAtRoute("GetSetor", new { id = setor.Id}, resultado);  
        }
         [HttpPut("{id}")]
        public ActionResult<RetornoView<Setor>> Update(int id, [FromBody] Setor setor)
        {
            var _setor = _setorRepository.Find(id);
            if(_setor == null)
            {
                return NotFound();
            }        
            try 
            {
                _setor.Nome_Setor   = setor.Nome_Setor;
                _setor.Numero_Leito = setor.Numero_Leito; 
                _setorRepository.Update(_setor);
            }
             catch (Exception ex)
            {
                var result = new RetornoView<Setor>() { sucesso = false, erro = ex.Message };
                return BadRequest(result);
            }

            var resultado = new RetornoView<Setor>() { data = _setor, sucesso = true };
            return resultado;
            
        }
        [HttpDelete("{id}")]
        public ActionResult<RetornoView<Setor>> Delete(int id) 
        {
            var setor  = _setorRepository.Find(id);
            if (setor == null) 
            {
                return NotFound();
            }
                _setorRepository.Remove(id);
               
            if (_setorRepository.Find(id) == null) 
            {
                var resultado = new RetornoView<Setor>() { sucesso = true };
                return resultado;
                
            }
            else 
            {
                var resultado = new RetornoView<Setor>() { sucesso = false };
                return BadRequest(resultado);
            }
        }
    }
}
    
