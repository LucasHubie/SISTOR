﻿using Microsoft.AspNetCore.Mvc;
using SISTOR.Configuration;
using SISTOR.Interfaces;
using SISTOR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static SISTOR.Interfaces.IFuncionarioRepositorio;

namespace SISTOR.Controllers
{
    public class FuncionarioController : Controller
    {
        private readonly AppDbContext _context;
        private IFuncionarioRepositorio _funcionarioRepositorio;

        public FuncionarioController(AppDbContext context, IFuncionarioRepositorio funcionarioRepositorio)
        {
            _context = context;
            _funcionarioRepositorio = funcionarioRepositorio;
        }

        public IActionResult Index(int pageNumber, int pageSize)
        {
            retornoFuncionarios retorno = _funcionarioRepositorio.GetFuncionarios(pageNumber, pageSize);
            return Json(new { lst = retorno.lst, qntdRegistros = retorno.qntdRegistros });
        }

        [HttpPost]
        public IActionResult CriarFuncionario([FromBody] Funcionario funcionario)
        {
            bool sucess = true;
            var description = "";
            funcionario.Id = 0;
            funcionario.IdPessoa = 0;
            try
            {
                _funcionarioRepositorio.CriarFuncionario(funcionario);
                sucess = true;
                description = "Funcionário criado com sucesso!";
                return Json(new { sucess = sucess, description = description });
            }
            catch (Exception ex)
            {
                sucess = false;
                description = "Funcionário não criado";
                return Json(new { sucess = sucess, description = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult EditarFuncionario([FromBody] Funcionario funcionario)
        {
            bool sucess = true;
            var description = "";

            try
            {
                _funcionarioRepositorio.EditarFuncionario(funcionario);
                sucess = true;
                description = "Funcionário atualizado com sucesso!";
                return Json(new { sucess = sucess, description = description });
            }
            catch (Exception ex)
            {
                sucess = false;
                description = "Funcionário não atualizado";
                return Json(new { sucess = sucess, description = ex.Message });
            }
        }

        public IActionResult GetFuncionarioById(int id)
        {
            bool sucess = true;
            var description = "";

            try
            {
                var funcionario = _funcionarioRepositorio.GetFuncionarioById(id);
                sucess = true;
                description = "Funcionario encontrado com sucesso!";
                return Json(new { sucess = sucess, description = description, funcionario = funcionario });
            }
            catch (Exception ex)
            {
                sucess = false;
                description = "Funcionario não encontrado criado";
                return Json(new { sucess = sucess, description = ex.Message });
            }

        }

        public IActionResult ExcluirFuncionario(int id)
        {
            bool sucess = true;
            var description = "";
            try
            {
                var funcionario = _funcionarioRepositorio.GetFuncionarioById(id);
                if (funcionario != null)
                {
                    _funcionarioRepositorio.ExcluirFuncionario(funcionario.Id);
                    sucess = true;
                    description = "Funcionário excluido com sucesso!";
                    return Json(new { sucess = sucess, description = description });
                }
                else
                {
                    sucess = false;
                    description = "Funcionário não encontrado";
                    return Json(new { sucess = sucess, description = description });
                }

            }
            catch (Exception ex)
            {
                sucess = false;
                description = "Funcionário não excluido";
                return Json(new { sucess = sucess, description = ex.Message });
            }
        }

        public IActionResult BuscarFuncionario(string busca)
        {
            bool sucess = true;
            var description = "";

            try
            {
                List<Funcionario> obj = new List<Funcionario>();
                obj = _funcionarioRepositorio.BuscarFuncionario(busca);
                if (obj != null)
                {
                    sucess = true;
                    description = "Funcionário encontrado com sucesso!";
                    return Json(new { sucess = sucess, description = description, funcionario = obj });
                }
                else
                {
                    sucess = false;
                    description = "Funcionário não encontrado";
                    return Json(new { sucess = sucess, description = description });
                }
            }
            catch (Exception ex)
            {
                sucess = false;
                description = "Funcionário não encontrado";
                return Json(new { sucess = sucess, description = ex.Message });
            }
        }

        public IActionResult GetFuncionario()
        {
            List<Funcionario> funcionario = _funcionarioRepositorio.GetFuncionarios();
            return Json(funcionario);
        }

        public IActionResult GetTotalFuncionario()
        {
            int funcionario = _funcionarioRepositorio.GetTotalFuncionario();
            return Json(funcionario);
        }

    }
}
