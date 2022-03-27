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
        public IActionResult CriarProduto([FromBody] Produto produto)
        {
            bool sucess = true;
            var description = "";
            produto.Id = 0;
            produto.Codigo = 0;
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

        public IActionResult BuscaProduto(string busca)
        {
            bool sucess = true;
            var description = "";

            try
            {
                List<Produto> obj = new List<Produto>();
                obj = _produtoRepositorio.BuscaProduto(busca);
                if (obj != null)
                {
                    sucess = true;
                    description = "Produto encontrado com sucesso!";
                    return Json(new { sucess = sucess, description = description, produtos = obj });
                }
                else
                {
                    sucess = false;
                    description = "Produto não encontrado";
                    return Json(new { sucess = sucess, description = description });
                }
            }
            catch (Exception ex)
            {
                sucess = false;
                description = "Produto não encontrado";
                return Json(new { sucess = sucess, description = ex.Message });
            }
        }

        public IActionResult GetProdutoById(int id)
        {
            bool sucess = true;
            var description = "";

            try
            {
                var produto = _produtoRepositorio.GetProdutoById(id);
                sucess = true;
                description = "Produto encontrado com sucesso!";
                return Json(new { sucess = sucess, description = description, produto = produto });
            }
            catch (Exception ex)
            {
                sucess = false;
                description = "Produto não encontrado criado";
                return Json(new { sucess = sucess, description = ex.Message });
            }

        }

        [HttpPost]
        public IActionResult EditarProduto([FromBody] Produto produto)
        {
            bool sucess = true;
            var description = "";

            try
            {
                _produtoRepositorio.EditarProduto(produto);
                sucess = true;
                description = "Produto atualizado com sucesso!";
                return Json(new { sucess = sucess, description = description });
            }
            catch (Exception ex)
            {
                sucess = false;
                description = "Produto não atualizado";
                return Json(new { sucess = sucess, description = ex.Message });
            }
        }

        public IActionResult ExcluirProduto(int id)
        {
            bool sucess = true;
            var description = "";
            try
            {
                var produto = _produtoRepositorio.GetProdutoById(id);
                if (produto != null)
                {
                    _produtoRepositorio.ExcluirProduto(produto.Id);
                    sucess = true;
                    description = "Produto excluido com sucesso!";
                    return Json(new { sucess = sucess, description = description });
                }
                else
                {
                    sucess = false;
                    description = "Produto não encontrado";
                    return Json(new { sucess = sucess, description = description });
                }

            }
            catch (Exception ex)
            {
                sucess = false;
                description = "Produto não excluido";
                return Json(new { sucess = sucess, description = ex.Message });
            }
        }

        public IActionResult GetProduto()
        {
            List<Produto> produto = _produtoRepositorio.GetProduto();
            return Json(produto);
        }

    }
}
