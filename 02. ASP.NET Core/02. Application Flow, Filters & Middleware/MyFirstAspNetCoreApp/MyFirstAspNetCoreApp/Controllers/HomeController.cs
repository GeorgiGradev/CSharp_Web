using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MyFirstAspNetCoreApp.Data;
using MyFirstAspNetCoreApp.Models;
using MyFirstAspNetCoreApp.Services;
using MyFirstAspNetCoreApp.ViewModels.Home;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstAspNetCoreApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext db;

        public HomeController(ILogger<HomeController> logger
            , ApplicationDbContext db)
        {
            _logger = logger;
            this.db = db;
        }

        public IActionResult Exeption()
        {
            throw new Exception();

        }
        public IActionResult Index(int id)
        {
  
            var viewModel1 = new IndexViewModel(); // създаваме инстанция на View Modela
            viewModel1.Year = DateTime.UtcNow.Year;
            viewModel1.Name = "Ivan";
            viewModel1.Id = id; // прочетено от заявката 
            viewModel1.ProcessorsCount = Environment.ProcessorCount;
            viewModel1.UsersCount = this.db.Users.Count();

            // или съкратено
            var viewModel = new IndexViewModel
            {
                Description = "A free Bootstrap 4 admin theme built with HTML/CSS and a modern development workflow environment ready to use to build your next dashboard or web application",
                Year = DateTime.UtcNow.Year,
                Name = "Ivan",
                Id = id, // прочетено от заявката 
                ProcessorsCount = Environment.ProcessorCount,
                UsersCount = this.db.Users.Count()
            };

            return View(viewModel); // тук подаваме ViewModel-a
        }

        public IActionResult StatusCodeError(int errorCode)
        {
            return this.View();
        } 

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
