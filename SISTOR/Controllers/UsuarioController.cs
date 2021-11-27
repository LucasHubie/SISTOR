using Microsoft.AspNetCore.Mvc;
using SISTOR.Configuration;
using SISTOR.Interfaces;
using SISTOR.Models;
using SISTOR.Repository;
using SISTOR.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SISTOR.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly AppDbContext _context;
        private IUsuarioRepositorio _usuarioRepositorio;

        public UsuarioController(AppDbContext context, IUsuarioRepositorio usuarioRepositorio)
        {
            _context = context;
            _usuarioRepositorio = usuarioRepositorio;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create(Usuario user)
        {
            _usuarioRepositorio.CriarUsuario(user);
            return Json(new { user = user});
        }
    }
}
