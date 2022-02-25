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
    public class FuncionarioController : Controller
    {
        private readonly AppDbContext _context;
        private IFuncionarioRepositorio _funcionarioRepositorio;

        public FuncionarioController(AppDbContext context, IFuncionarioRepositorio funcionarioRepositorio)
        {
            _context = context;
            _funcionarioRepositorio = funcionarioRepositorio;
        }
        //public IActionResult Index()
        //{
        //    List<Pessoa> funcionario = _funcionarioRepositorio.GetFuncionario();
        //    return Json(funcionario);
        //}

        public IActionResult Index()
        {
            List<Funcionario> funcionario = _funcionarioRepositorio.GetFuncionarios();
            return Json(funcionario);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Funcionario funcionario)
        {
            bool sucess = true;
            var description = "";

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

        //public IActionResult CriarFuncionario(Pessoa funcionario)
        //{
        //    bool sucess = true;
        //    var description = "";

        //    if (funcionario != null)
        //    {
        //        _funcionarioRepositorio.CriarFuncionario(funcionario);
        //        sucess = true;
        //        description = "Funcionário criado com sucesso!";
        //        return Json(new { sucess = sucess, description = description });
        //    }
        //    else
        //    {
        //        sucess = false;
        //        description = "Funcionário não criado";
        //        return Json(new { sucess = sucess, description = description });
        //    }
        //}
    }
}
