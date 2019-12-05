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
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Bangazon.Controllers
{
    public class ReportsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _config;

        public SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }

        public ReportsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IConfiguration config)
        {
            _context = context;
            _userManager = userManager;
            _config = config;

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

        // Get users with multiple orders.
        public async Task<IActionResult> UsersWithMultipleOrders()
        {
            var model = new UsersWithMultipleOrdersViewModel();

            //model.Users = await _context
            //    .ApplicationUsers
            //    .Include(a => a.Orders)
            //    .Where(a => a.Orders.Count > 1 && (a.PaymentTypes == null)) 
            //    .ToListAsync();

            model.Orders = await _context.Order
                .Include(o => o.User)
                .Where(o => o.PaymentTypeId == null)
                .ToListAsync();
            

            return View(model);
        }

            //return View(model);
            //using (SqlConnection conn = Connection)
            //{
            //    conn.Open();
            //    using (SqlCommand cmd = conn.CreateCommand())
            //    {
            //        cmd.CommandText = @"
            //            select a.FirstName, a.LastName, COUNT(o.UserId) as OpenOrders from AspNetUsers a
            //            left join [Order] o 
            //            on o.UserId = a.Id
            //            where o.PaymentTypeId is null
            //            group by a.FirstName, a.LastName";
            //        SqlDataReader reader = cmd.ExecuteReader();

            //        var users = new List<ApplicationUser>();
            //        while (reader.Read())
            //        {
            //            users.Add(new ApplicationUser
            //            {
            //                Id = reader.GetString(reader.GetOrdinal("Id")),
            //                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
            //                LastName = reader.GetString(reader.GetOrdinal("LastName")),
            //                Orders = reader.GetInt32(reader.GetOrdinal("OpenOrders"))
            //            });
            //        }

            //        reader.Close();

            //        return users;
            //    }
            //}


            private bool OrderExists(int id)
            {
                return _context.Order.Any(e => e.OrderId == id);
            }
        }
    }
