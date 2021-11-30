using Microsoft.AspNetCore.Cors;
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
    public class OrcamentoController : Controller
    {
        private readonly AppDbContext _context;
        private IOrcamentoRepositorio _orcamentoRepositorio;


        public OrcamentoController(AppDbContext context, IOrcamentoRepositorio orcamentoRepositorio)
        {
            _context = context;
            _orcamentoRepositorio = orcamentoRepositorio;
        }


        public IActionResult Index()
        {
            List<Orcamento> orc = _orcamentoRepositorio.GetOrcamentos();
            return Json(orc);
        }

        public IActionResult GridOS()
        {
            List<OrdemServico> orc = _orcamentoRepositorio.GetOrdensServico();
            return Json(orc);
        }
        

        public IActionResult GetOrcamentoById(int id)
        {
            bool sucess = true;
            var description = "";

            try
            {
             
                OrcamentoVM obj = new OrcamentoVM();
                obj = _orcamentoRepositorio.GetOrcamentoById(id);
                if (obj != null)
                {
                    sucess = true;
                    description = "Orcamento encontrado com sucesso!";
                    return Json(new { sucess = sucess, description = description, obj = obj });
                }
                else
                {
                    sucess = false;
                    description = "Orcamento não encontrado";
                    return Json(new { sucess = sucess, description = description });
                }
            }
            catch (Exception ex)
            {
                sucess = false;
                description = "Cliente não encontrado";
                return Json(new { sucess = sucess, description = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody]OrcamentoVM obj)
        {
            bool sucess = true;
            var description = "";

            try
            {
                _orcamentoRepositorio.CriarOrcamento(obj);
                sucess = true;
                description = "Orcamento criado com sucesso!";
                return Json(new { sucess = sucess, description = description });
            }
            catch (Exception ex)
            {
                sucess = false;
                description = "Orcamento não criado";
                return Json(new { sucess = sucess, description = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Editar([FromBody] OrcamentoVM obj)
        {
            bool sucess = true;
            var description = "";
            try
            {
                _orcamentoRepositorio.UpdateOrcamento(obj);
                sucess = true;
                description = "Orcamento alterado com sucesso!";
                return Json(new { sucess = sucess, description = description });
            }
            catch (Exception ex)
            {
                sucess = false;
                description = "Orcamento não alterado";
                return Json(new { sucess = sucess, description = ex.Message });
            }
        }

        public IActionResult Excluir(int id)
        {
            bool sucess = true;
            var description = "";
            try
            {
                _orcamentoRepositorio.Delete(id);
                sucess = true;
                description = "Orcamento excluido com sucesso!";
                return Json(new { sucess = sucess, description = description });
            }
            catch (Exception ex)
            {
                sucess = false;
                description = "Orcamento não excluido";
                return Json(new { sucess = sucess, description = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult CriaOrdemServico([FromBody] OrdemServico obj)
        {
            bool sucess = true;
            var description = "";

            try
            {
                _orcamentoRepositorio.CriarOrdemServico(obj);
                sucess = true;
                description = "Orcamento criado com sucesso!";
                return Json(new { sucess = sucess, description = description });
            }
            catch (Exception ex)
            {
                sucess = false;
                description = "Orcamento não criado";
                return Json(new { sucess = sucess, description = ex.Message });
            }
        }
    }
}
