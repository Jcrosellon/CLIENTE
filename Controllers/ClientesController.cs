using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CLIENTE.Data;
using CLIENTE.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLIENTE.Controllers
{
    [ApiController]
    [Route("api/clientes/search")]
    public class ClientesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClientesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {
            return await _context.Clientes.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return cliente;
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Cliente>>> SearchClientes(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                return BadRequest("Keyword is required");
            }

#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var clientes = await _context.Clientes
                                         .Where(c => c.Nombre.Contains(keyword) || c.NIT.Contains(keyword))
                                         .ToListAsync();
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            return Ok(clientes);
        }


        // Otros métodos (POST, PUT, DELETE)...
    }
}


