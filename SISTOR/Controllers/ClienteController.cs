using Microsoft.AspNetCore.Mvc;
using SISTOR.Configuration;
using SISTOR.Interfaces;
using SISTOR.Models;

namespace SISTOR.Controllers
{
    public class ClienteController : Controller
    {
        private readonly AppDbContext _context;
        private IClienteRepositorio _usuarioRepositorio;

        public ClienteController(AppDbContext context, IClienteRepositorio usuarioRepositorio)
        {
            _context = context;
            _usuarioRepositorio = usuarioRepositorio;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [DisableCors]
        public IActionResult Create(Cliente user)
        {
            _usuarioRepositorio.CriarCliente(user);
            return Json(new { user = user});
        }
    }
}
