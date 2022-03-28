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
using static SISTOR.Interfaces.IOrcamentoRepositorio;

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

        public IActionResult GetOrcamentos(string busca,int pageNumber, int pageSize)
        {
            retornoOrcamentos retorno = new retornoOrcamentos();
            if (!String.IsNullOrEmpty(busca))
            {
                retorno = _orcamentoRepositorio.BuscarOrcamento(busca ,pageNumber, pageSize);
            }
            else
            {
                retorno = _orcamentoRepositorio.GetOrcamentos(pageNumber, pageSize);
            }
            return Json(new { lst = retorno.lst, qntdRegistros = retorno.qntdRegistros });
        }

        public IActionResult GetItensOrcamento()
        {
            List<Itens> itens = _orcamentoRepositorio.GetItensOrcamento();
            return Json(itens);
        }

        public IActionResult GetItensOrcamentoById(int id)
        {
            List<Itens> itens = _orcamentoRepositorio.GetItensOrcamentoById(id);
            return Json(itens);
        }

        public IActionResult BuscarOrcamento(string busca, int pageNumber, int pageSize)
        {
            bool sucess = true;
            var description = "";

            try
            {
                retornoOrcamentos retorno = _orcamentoRepositorio.BuscarOrcamento(busca, pageNumber, pageSize);
                if (retorno != null)
                {
                    sucess = true;
                    description = "Orçamento encontrado com sucesso!";
                    return Json(new { sucess = sucess, description = description, orcamentos = retorno.lst, qntdRegistros = retorno.qntdRegistros });
                }
                else
                {
                    sucess = false;
                    description = "Orçamento não encontrado";
                    return Json(new { sucess = sucess, description = description });
                }
            }
            catch (Exception ex)
            {
                sucess = false;
                description = "Orçamento não encontrado";
                return Json(new { sucess = sucess, description = ex.Message });
            }
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
        public IActionResult CriarOrcamento([FromBody]OrcamentoVM obj)
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
        public IActionResult EditarOrcamento([FromBody] OrcamentoVM obj)
        {
            bool sucess = true;
            var description = "";
            try
            {
                _orcamentoRepositorio.EditarOrcamento(obj);
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

        public IActionResult ExcluirOrcamento(int id)
        {
            bool sucess = true;
            var description = "";
            try
            {
                _orcamentoRepositorio.ExcluirOrcamento(id);
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
                description = "Ordem de Servico criado com sucesso!";
                return Json(new { sucess = sucess, description = description });
            }
            catch (Exception ex)
            {
                sucess = false;
                description = "Ordem de Servico não criado";
                return Json(new { sucess = sucess, description = ex.Message });
            }
        }

        public IActionResult GetOrcamentoSituacao()
        {
            int orcamentos = _orcamentoRepositorio.GetOrcamentoAprovado();
            int orcamentosAguardando = _orcamentoRepositorio.GetOrcamentoAguardando();
            return Json(new { orcamentos = orcamentos, orcamentosAguardando = orcamentosAguardando });
        }

    }
}
