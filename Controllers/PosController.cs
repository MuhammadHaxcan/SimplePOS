using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimplePOS.Data;
using SimplePOS.Services;
using SimplePOS.ViewModels;

namespace SimplePOS.Controllers
{

    public class PosController : Controller
    {
        private readonly IPosService _posService;
        private readonly AppDbContext _db;

        public PosController(IPosService posService, AppDbContext db)
        {
            _posService = posService;
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var vm = new PosViewModel
            {
                Products = await _db.Products.OrderBy(p => p.Name).ToListAsync(),
                Customers = await _db.Customers.OrderBy(c => c.Name).ToListAsync()
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderVm vm)
        {
            try
            {
                var order = await _posService.CreateOrderAsync(vm);
                return Json(new { success = true, orderId = order.Id });
            }
            catch (Exception ex)
            {
                // Return structured error for UI; unified handling will log this too
                return BadRequest(new { success = false, error = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Invoice(int id)
        {
            var order = await _db.Orders
                .Include(o => o.Customer)
                .Include(o => o.Items).ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null) return NotFound();

            return View(order); 
        }

        [HttpGet]
        public async Task<IActionResult> Restock()
        {
            var products = await _db.Products.ToListAsync();
            ViewBag.Products = products;
            return View(new RestockItemVm());
        }

        [HttpPost]
        public async Task<IActionResult> Restock(RestockItemVm vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Products = await _db.Products.ToListAsync();
                return View(vm);
            }

            await _posService.RestockAsync(vm);
            TempData["Success"] = "Product restocked successfully.";
            return RedirectToAction("Index");
        }
    }

}
