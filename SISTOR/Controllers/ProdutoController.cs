using Microsoft.AspNetCore.Mvc;
using SISTOR.Configuration;
using SISTOR.Interfaces;
using SISTOR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static SISTOR.Interfaces.IProdutoRepositorio;

namespace SISTOR.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly AppDbContext _context;
        private IProdutoRepositorio _produtoRepositorio;
        public ProdutoController(AppDbContext context, IProdutoRepositorio produtoRepositorio)
        {
            _context = context;
            _produtoRepositorio = produtoRepositorio;
        }
        public IActionResult Index(int pageNumber, int pageSize)
        {
            retornoProdutos retorno = _produtoRepositorio.GetProdutos(pageNumber, pageSize);
            return Json(new { lst = retorno.lst, qntdRegistros = retorno.qntdRegistros });
        }

        [HttpPost]
        public IActionResult Create(Produto produto)
        {
            bool sucess = true;
            var description = "";
            //produto.Id = 0;
            try
            {
                _produtoRepositorio.CriarProduto(produto);
                sucess = true;
                description = "Produto criado com sucesso!";
                return Json(new { sucess = sucess, description = description });
            }
            catch (Exception ex)
            {
                sucess = false;
                description = "Produto não criado";
                return Json(new { sucess = sucess, description = ex.Message });
            }
        }

    }
}
