using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLIENTE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly YourDbContext _context;

        public PedidosController(YourDbContext context)
        {
            _context = context;
        }

        // GET: api/pedidos/search?keyword=value&date=value
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<PedidoDto>>> SearchPedidos(string keyword, string date)
        {
            var pedidos = _context.Pedidos.Include(p => p.Cliente).AsQueryable();

            if (!string.IsNullOrEmpty(keyword))
            {
                pedidos = pedidos.Where(p => p.Cliente.Nombre.Contains(keyword) || p.Cliente.Nit.Contains(keyword));
            }

            if (!string.IsNullOrEmpty(date))
            {
                if (DateTime.TryParse(date, out DateTime parsedDate))
                {
                    pedidos = pedidos.Where(p => p.Date.Date == parsedDate.Date);
                }
            }

            var pedidosList = await pedidos.Select(p => new PedidoDto
            {
                Id = p.Id,
                ClienteNombre = p.Cliente.Nombre,
                ClienteNit = p.Cliente.Nit,
                Date = p.Date,
                StatusDate = p.StatusDate,
                PreparingDate = p.PreparingDate,
                ShippedDate = p.ShippedDate,
                DeliveredDate = p.DeliveredDate,
                Total = p.Total
            }).ToListAsync();

            return Ok(pedidosList);
        }
    }

    public class PedidoDto
    {
        public int Id { get; set; }
        public string ClienteNombre { get; set; }
        public string ClienteNit { get; set; }
        public DateTime Date { get; set; }
        public DateTime? StatusDate { get; set; }
        public DateTime? PreparingDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public DateTime? DeliveredDate { get; set; }
        public decimal Total { get; set; }
    }
}
