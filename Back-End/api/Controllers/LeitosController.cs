using System;
using api.Models;
using api.Repository;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers

{
    [Route("api/[Controller]")]
    public class LeitosController : Controller
    {
      private readonly ILeitosRepository _leitosRepository;
        public LeitosController(ILeitosRepository leitosRepo)
        {
            _leitosRepository = leitosRepo;
        }  
         [HttpGet]
        public ActionResult<RetornoView<Leitos>> GetAll()
        {
            return Ok (new{data = _leitosRepository.GetAll()});
        }  
        
        [HttpGet("{id}", Name = "GetLeitos")]
        public ActionResult<RetornoView<Leitos>> GetById(int id)
        {
            
            var leitos = _leitosRepository.Find(id);
            if(leitos == null)
            {
                return NotFound();           
            }
            return Ok( new {data = leitos});
        }
        [HttpPost]
        public ActionResult<RetornoView<Leitos>> Create ([FromBody]Leitos leitos)
        {
            var usuarioLogado = _leitosRepository.usuarioLogado(User.Identity.Name);
            try
            {               
                leitos.Ocupado    = true;
                if(leitos.Ocupado == true)
                {
                    leitos.Data_Entrada = DateTime.Now;
                }
                _leitosRepository.Add(leitos);
 
            }
            catch (Exception ex) 
            {
                var result = new RetornoView<Leitos>() { sucesso = false, erro = ex.Message };
                return BadRequest(result);
            }

            var resultado = new RetornoView<Leitos>() { data = leitos, sucesso = true };
            return CreatedAtRoute("GetLeitos", new { id = leitos.Id}, resultado);  
        }
         [HttpPut("{id}")]
        public ActionResult<RetornoView<Leitos>> Update(int id, [FromBody] Leitos leitos)
        {
            var _leitos = _leitosRepository.Find(id);
            if(_leitos == null)
            {
                return NotFound();
            }        
            try 
            {
               
                _leitos.pacientes.Nome          = leitos.pacientes.Nome;
                _leitos.pacientes.Acompanhante  = leitos.pacientes.Acompanhante;
                
                if (leitos.Ocupado == false)
                {
                    leitos.Data_Saida = DateTime.Now;
                }
               
                _leitosRepository.Update(_leitos);
            }
             catch (Exception ex)
            {
                var result = new RetornoView<Leitos>() { sucesso = false, erro = ex.Message };
                return BadRequest(result);
            }

            var resultado = new RetornoView<Leitos>() { data = _leitos, sucesso = true };
            return resultado;
            
        }
        [HttpDelete("{id}")]
        public ActionResult<RetornoView<Leitos>> Delete(int id) 
        {
            var leitos  = _leitosRepository.Find(id);
            if (leitos == null) 
            {
                return NotFound();
            }
                
                _leitosRepository.Remove(id);
                
                    
            if (_leitosRepository.Find(id) == null) 
            {
                var resultado = new RetornoView<Leitos>() { sucesso = true };
                return resultado;
                
            }
            else 
            {
                var resultado = new RetornoView<Leitos>() { sucesso = false };
                return BadRequest(resultado);
            }
        }
    }
}
    
