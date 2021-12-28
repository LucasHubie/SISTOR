using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SISTOR.Configuration;
using SISTOR.Models;
using SISTOR.Service;
using SISTOR.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SISTOR.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            //List<UF> uf = UFService.GetEstados();
            //foreach (var item in uf)
            //{
            //    _context.Add(item);
            //}
            //_context.SaveChanges();
            return View();
        }

       

        public IActionResult Privacy()
        {
            List<Object> resultado = new List<object>();
            resultado.Add(new
            {
                Nome = "Linha de Código",
                URL = "www.linhadecodigo.com.br"
            });
            return Json(resultado);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
