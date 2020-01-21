using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Distrito")]
    [ApiController]
    public class DistritoController : ControllerBase
    {
        private readonly ApplicationDBContext context;

        public DistritoController(ApplicationDBContext context)
        {
            this.context = context;
        } 

        [HttpGet]
        public IEnumerable<Distrito> ListaDistrito()
        {
            return context.Distritos.ToList();
        }
        
        [HttpGet("{id}", Name ="distritoCreado")]
        public IActionResult ListaDistritoById(int id)
        {
            var sDistrito = context.Distritos.FirstOrDefault(x => x.Id == id);

            if (sDistrito == null)
            {
                return NotFound();
            }

            return Ok(sDistrito);
            
        }

        [HttpPost]
        public IActionResult Post([FromBody] Distrito distrito)
        {
            if (ModelState.IsValid)
            {
                context.Distritos.Add(distrito);
                context.SaveChanges();
                return new CreatedAtRouteResult("distritoCreado", new
                {
                    id = distrito.Id
                }, distrito);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Distrito distrito, int id)
        {

            if (distrito.Id != id)
            {
                return BadRequest();
            }

            context.Entry(distrito).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromBody] Distrito distrito, int id)
        {

            var sdistrito = context.Distritos.FirstOrDefault(x => x.Id == id);

            if (sdistrito == null)
            {
                return BadRequest();
            }

            context.Distritos.Remove(sdistrito);
            context.SaveChanges();
            return Ok();

        }

    }
}