using System;
using api.Models;
using api.Repository;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers

{
    [Route("api/[Controller]")]
    public class PacientesController : Controller
    {
      private readonly IPacientesRepository _pacientesRepository;
        public PacientesController(IPacientesRepository pacientesRepo)
        {
            _pacientesRepository = pacientesRepo;
        }  
         [HttpGet]
        public ActionResult<RetornoView<Pacientes>> GetAll()
        {
            return Ok (new{data = _pacientesRepository.GetAll()});
        }  
        
        [HttpGet("{id}", Name = "GetPacientes")]
        public ActionResult<RetornoView<Pacientes>> GetById(int id)
        {
            
            var pacientes = _pacientesRepository.Find(id);
            if(pacientes == null)
            {
                return NotFound();           
            }
            return Ok( new {data = pacientes});
        }
        [HttpPost]
        public ActionResult<RetornoView<Pacientes>> Create ([FromBody]Pacientes pacientes)
        {
            //var usuarioLogado = _usuarioRepository.usuarioLogado(User.Identity.Name);
            try
            {
                pacientes.Data_Internacao = DateTime.Now;
                _pacientesRepository.Add(pacientes);
            }
            catch (Exception ex) 
            {
                var result = new RetornoView<Pacientes>() { sucesso = false, erro = ex.Message };
                return BadRequest(result);
            }

            var resultado = new RetornoView<Pacientes>() { data = pacientes, sucesso = true };
            return CreatedAtRoute("GetPacientes", new { id = pacientes.Id}, resultado);  
        }
         [HttpPut("{id}")]
        public ActionResult<RetornoView<Pacientes>> Update(int id, [FromBody] Pacientes pacientes)
        {
            var _pacientes = _pacientesRepository.Find(id);
            if(_pacientes == null)
            {
                return NotFound();
            }        
            try 
            {
                _pacientes.Nome                 = pacientes.Nome;
                _pacientes.Acompanhante         = pacientes.Acompanhante;
                _pacientes.Telefone             = pacientes.Telefone;
                /*_pacientes.endereco.rua         = pacientes.endereco.rua;
                _pacientes.endereco.numero      = pacientes.endereco.numero;
                _pacientes.endereco.complemento = pacientes.endereco.complemento;
                _pacientes.endereco.bairro      = pacientes.endereco.bairro;
                _pacientes.endereco.cidade      = pacientes.endereco.cidade;
                _pacientes.endereco.estado      = pacientes.endereco.estado;
                _pacientes.endereco.uf          = pacientes.endereco.uf;*/
                
                _pacientesRepository.Update(_pacientes);
            }
             catch (Exception ex)
            {
                var result = new RetornoView<Pacientes>() { sucesso = false, erro = ex.Message };
                return BadRequest(result);
            }

            var resultado = new RetornoView<Pacientes>() { data = _pacientes, sucesso = true };
            return resultado;
            
        }
        [HttpDelete("{id}")]
        public ActionResult<RetornoView<Pacientes>> Delete(int id) 
        {
            var pacientes  = _pacientesRepository.Find(id);
            if (pacientes == null) 
            {
                return NotFound();
            }
                
                _pacientesRepository.Remove(id);
            
                    
            if (_pacientesRepository.Find(id) == null) 
            {
                var resultado = new RetornoView<Pacientes>() { sucesso = true };
                return resultado;
                
            }
            else 
            {
                var resultado = new RetornoView<Pacientes>() { sucesso = false };
                return BadRequest(resultado);
            }
        }
    }
}
    
