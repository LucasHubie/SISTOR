using Microsoft.AspNetCore.Mvc;
using SISTOR.Configuration;
using SISTOR.Interfaces;
using SISTOR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SISTOR.Controllers
{
    public class OrdemServicoController : Controller
    {
        private readonly AppDbContext _context;
        private IOrdemServicoRepositorio _ordemServicoRepositorio;
        public OrdemServicoController(AppDbContext context, IOrdemServicoRepositorio ordemServicoRepositorio)
        {
            _context = context;
            _ordemServicoRepositorio = ordemServicoRepositorio;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetOrdemServico()
        {
            List<OrdemServico> os = _ordemServicoRepositorio.GetOrdemServico();
            return Json(os);
        }

        public IActionResult GetOrdemServicoById(int id)
        {
            bool sucess = true;
            var description = "";
            try
            {
                var ordemServico = _ordemServicoRepositorio.GetOrdemServioById(id);
                sucess = true;
                description = "Ordem de serviço encontrada com sucesso!";
                return Json(new { sucess = sucess, description = description, ordemServico = ordemServico });
            }
            catch (Exception ex)
            {
                sucess = false;
                description = "Ordem de serviço não encontrada";
                return Json(new { sucess = sucess, description = ex.Message });
            }
        }
    }
}
