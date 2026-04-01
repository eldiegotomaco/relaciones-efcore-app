using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RelacionesEFCoreApp.Data;
using RelacionesEFCoreApp.Models;

namespace RelacionesEFCoreApp.Controllers
{
    public class PedidoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PedidoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 🔹 LISTAR (Include obligatorio)
        public async Task<IActionResult> Index()
        {
            var pedidos = _context.Pedidos.Include(p => p.Cliente);
            return View(await pedidos.ToListAsync());
        }

        // 🔹 CREATE (GET)
        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Nombre");
            ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Nombre");
            return View();
        }

        // 🔹 CREATE (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Pedido pedido, int ProductoId, int Cantidad)
        {
            pedido.FechaPedido = DateTime.Now;

            // 1️⃣ Guardamos pedido primero
            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();

            // 2️⃣ Obtenemos producto
            var producto = await _context.Productos.FindAsync(ProductoId);

            // 3️⃣ Creamos detalle
            var detalle = new DetallePedido
            {
                PedidoId = pedido.Id,
                ProductoId = ProductoId,
                Cantidad = Cantidad,
                Precio = producto.Precio
            };

            _context.DetallesPedidos.Add(detalle);

            // 🔥 4️⃣ AQUÍ SE CALCULA EL TOTAL
            pedido.Total = Cantidad * producto.Precio;

            // 5️⃣ Guardamos todo
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // 🔹 DELETE
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var pedido = await _context.Pedidos
                .Include(p => p.Cliente)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (pedido == null) return NotFound();

            return View(pedido);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            _context.Pedidos.Remove(pedido);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}