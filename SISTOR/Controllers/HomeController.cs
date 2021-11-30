using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SISTOR.Models;
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

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        //public IActionResult Login(UsuarioVM user)
        //{
        //    bool resultado = true;
        //    if (user.Nome == "Lucas" || user.Senha == "123")
        //    {
        //         resultado = true;
        //    }
        //    else
        //    {
        //        resultado = false;
        //    }
        //    return Json(resultado);
        //}

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
