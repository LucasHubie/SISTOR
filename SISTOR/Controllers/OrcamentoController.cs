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
    public class OrcamentoController : Controller
    {
        private readonly AppDbContext _context;
        private IOrcamentoRepositorio _usuarioRepositorio;

        public OrcamentoController(AppDbContext context, IOrcamentoRepositorio usuarioRepositorio)
        {
            _context = context;
            _usuarioRepositorio = usuarioRepositorio;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create(Orcamento user)
        {
            _usuarioRepositorio.CriarOrcamento(user);
            return Json(new { user = user });
        }
    }
}
