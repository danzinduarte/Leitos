using System;
using api.Models;
using api.Repository;
using Microsoft.AspNetCore.Mvc;


namespace api.Controllers
{
    [Route("api/[Controller]")]
    public class LogController : Controller
    {
        private readonly ILogRepository _logRepository;
        public LogController(ILogRepository logRepo)
        {
            _logRepository = logRepo;
        }

        [HttpGet]
        public ActionResult<RetornoView<Log>> GetAll()
        {
            return Ok (new{data = _logRepository.GetAll()});
        }

        [HttpGet("{id}", Name = "GetLog")]
        public ActionResult<RetornoView<Log>> GetById(int id)
        {
            
            var log = _logRepository.Find(id);
            if(log==null)
            {
                return NotFound();           
            }
            return Ok( new {data = log});
        }
       
        [HttpPost]
        public ActionResult<RetornoView<Log>> Create ([FromBody] Log log)
        {
            try
            {
               _logRepository.Add(log);
            }
            catch (Exception ex)
            {
                var result = new RetornoView<Log>() { sucesso = false, erro = ex.Message };
                return BadRequest(result);
            }
            var resultado = new RetornoView<Log>() { data = log, sucesso = true };
            return CreatedAtRoute("GetLog", new { id = log.Id}, resultado);
        }
        
    }
}