using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CLIENTE.Data;
using CLIENTE.Models;

namespace CLIENTE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClientesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/clientes/search?keyword=value
        [HttpGet("search")]
public async Task<ActionResult<IEnumerable<PedidoDto>>> SearchPedidos(string keyword)
{
    var pedidosQuery = _context.Pedidos.Include(p => p.Cliente).AsQueryable();

    if (!string.IsNullOrEmpty(keyword))
    {
        pedidosQuery = pedidosQuery.Where(p => p.Cliente != null &&
                                   ((p.Cliente.Nombre != null && p.Cliente.Nombre.Contains(keyword)) ||
                                    (p.Cliente.NIT != null && p.Cliente.NIT.Contains(keyword))));
    }

    var pedidosList = await pedidosQuery.ToListAsync();

    var pedidosDtoList = pedidosList.Select(p => new PedidoDto
    {
        Id = p.Id,
        ClienteNombre = p.Cliente?.Nombre ?? string.Empty,
        ClienteNit = p.Cliente?.NIT ?? string.Empty,
        Date = p.Date,
        StatusDate = p.StatusDate,
        PreparingDate = p.PreparingDate,
        ShippedDate = p.ShippedDate,
        DeliveredDate = p.DeliveredDate,
        Total = p.Total
    }).ToList();

    return Ok(pedidosDtoList);
}
    }


    public class PedidoDto
    {
        public int Id { get; set; }
        public string ClienteNombre { get; set; } = string.Empty; // Inicializado para evitar advertencias
        public string ClienteNit { get; set; } = string.Empty;    // Inicializado para evitar advertencias
        public DateTime Date { get; set; }
        public DateTime? StatusDate { get; set; }
        public DateTime? PreparingDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public DateTime? DeliveredDate { get; set; }
        public decimal Total { get; set; }
    }
}


