using ETaskRep.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ETaskRep.Data;

namespace ETaskRep.Controllers
{
    public class UserController : Controller
    {
        private readonly IConfiguration _config;
        private readonly ApplicationDbContext _context;

        public UserController(IConfiguration config, ApplicationDbContext context)
        {
            _config = config;
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

     [HttpPost]
        public IActionResult Register(Registration model)
        {
            if (ModelState.IsValid)
            {
        
                var reg = new Registration
                {
                    FullName = model.FullName,
                    Email = model.Email,
                    PhoneNo = model.PhoneNo,
                    Username = model.Username,
                    Password = model.Password, 
                    ConfirmPassword = model.ConfirmPassword,
                    DateOfBirth = model.DateOfBirth,
                    Address = model.Address
                };

                _context.Register.Add(reg);
                _context.SaveChanges();

                TempData["Success"] = "Registration successful!";
                return RedirectToAction("Login", "User");
            }

            // If model is not valid, redisplay the form with errors
            return View(model);
        }
    

    [HttpPost]
        public IActionResult Login(User user)
        {
            string connStr = _config.GetConnectionString("DefaultConnection");
            using SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            string query = "SELECT * FROM Users WHERE Username=@Username AND Password=@Password";
            using SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Username", user.Username);
            cmd.Parameters.AddWithValue("@Password", user.Password);
            using SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                // User found, login success
                return RedirectToAction("Dashboard");
            }

            ViewBag.Error = "Invalid username or password.";
            return View();
        }
        public IActionResult Dashboard()
        {
            return Content("Welcome to the Dashboard!");
        }
    }

}
   
