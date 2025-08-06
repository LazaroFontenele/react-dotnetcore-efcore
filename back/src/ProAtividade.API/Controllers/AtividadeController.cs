using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProAtividade.API.Data;
using ProAtividade.API.Models;

namespace ProAtividade.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AtividadeController : ControllerBase
    {
        public readonly DataContext _context;
        public AtividadeController(DataContext context)
        {
            _context = context;

        }
        [HttpGet]
        public IEnumerable<Atividade> get()
        {

            return _context.Atividades;
        }

        [HttpGet("{id}")]
        public Atividade get(int id)
        {
            return _context.Atividades.FirstOrDefault(ativ => ativ.Id == id);
        }

        [HttpPost]
        public IActionResult Post(Atividade atividade)
        {
            _context.Atividades.Add(atividade);
            if (_context.SaveChanges() > 0)
                return Ok(_context.Atividades);
            else
                return BadRequest();

        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Atividade atividade)
        {
            if (atividade.Id != id) throw new Exception("Você está tentando atualizar a atividade errada");
            _context.Update(atividade);
            if (_context.SaveChanges() > 0)
                return Ok(_context.Atividades.FirstOrDefault(ativ => ativ.Id == id));
            else
                return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var atividade = _context.Atividades.FirstOrDefault(ativ => ativ.Id == id);
            if (atividade == null)
                throw new Exception("Você está tentando remover uma atividade que não existe");
            _context.Remove(atividade);
            return Ok(_context.SaveChanges() > 0);
        }
    }
}