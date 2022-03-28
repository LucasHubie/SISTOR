using Microsoft.AspNetCore.Mvc;
using SISTOR.Configuration;
using SISTOR.Interfaces;
using SISTOR.Models;
using SISTOR.ViewModels;
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

        [HttpPost]
        public IActionResult CriarOrdemServico([FromBody] OrdemServicoVM obj)
        {
            bool sucess = true;
            var description = "";

            try
            {
                _ordemServicoRepositorio.CriarOrdemServico(obj);
                sucess = true;
                description = "OrdemServico criado com sucesso!";
                return Json(new { sucess = sucess, description = description });
            }
            catch (Exception ex)
            {
                sucess = false;
                description = "OrdemServico não criado";
                return Json(new { sucess = sucess, description = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult EditarOrdemServico([FromBody] OrdemServicoVM obj)
        {
            bool sucess = true;
            var description = "";
            try
            {
                _ordemServicoRepositorio.EditarOrdemServico(obj);
                sucess = true;
                description = "Ordem de Servico alterado com sucesso!";
                return Json(new { sucess = sucess, description = description });
            }
            catch (Exception ex)
            {
                sucess = false;
                description = "Ordem de Servico não alterado";
                return Json(new { sucess = sucess, description = ex.Message });
            }
        }

        public IActionResult ExcluirOrdemServico(int id)
        {
            bool sucess = true;
            var description = "";
            try
            {
                _ordemServicoRepositorio.ExcluirOrdemServico(id);
                sucess = true;
                description = "OrdemServico excluido com sucesso!";
                return Json(new { sucess = sucess, description = description });
            }
            catch (Exception ex)
            {
                sucess = false;
                description = "OrdemServico não excluido";
                return Json(new { sucess = sucess, description = ex.Message });
            }
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

        public IActionResult GetOrdemServicoSituacao()
        {
            int ordemAberta = _ordemServicoRepositorio.GetOrdemAberta();
            int ordemFechada = _ordemServicoRepositorio.GetOrdemFechada();
            return Json(new { ordemAberta = ordemAberta, ordemFechada = ordemFechada });
        }

    }
}
