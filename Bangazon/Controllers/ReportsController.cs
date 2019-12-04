using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bangazon.Data;
using Bangazon.Models;
using Microsoft.AspNetCore.Identity;
using Bangazon.Models.OrderViewModels;

namespace Bangazon.Controllers
{
    public class ReportsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ReportsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // GET: Reports
        public async Task<IActionResult> Index()
        {
            var order = _context.Order.Include(o => o.PaymentType).Include(o => o.User).FirstOrDefaultAsync();
            return View(await order);
        }

        // Get the incomplete orders that have products the current user is selling and display their information
        public async Task<IActionResult> IncompleteOrders()
        {
            var user = await GetCurrentUserAsync();
            ViewData["UserId"] = user.Id;
            var viewModel = new IncompleteOrderViewModel();

            // Gets all the incomplete orders from the database
            var allIncompleteOrders = await _context.Order
                .Where(o => o.DateCompleted == null)
                .Include(o => o.User)
                .Include(o => o.OrderProducts)
                    .ThenInclude(o => o.Product)
                .ToListAsync();

            viewModel.Orders = allIncompleteOrders;

            return View(viewModel);
        }


        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.OrderId == id);
        }
    }
}
