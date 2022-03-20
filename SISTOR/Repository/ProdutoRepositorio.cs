using Microsoft.Data.SqlClient;
using SISTOR.Configuration;
using SISTOR.Interfaces;
using SISTOR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static SISTOR.Interfaces.IProdutoRepositorio;

namespace SISTOR.Repository
{
    public class ProdutoRepositorio : IProdutoRepositorio
    {
        private readonly AppDbContext _context;
        public ProdutoRepositorio(AppDbContext context)
        {
            _context = context;
        }

        public retornoProdutos GetProdutos(int pageNumber, int pageSize)
        {
            retornoProdutos objretorno = new retornoProdutos();
            var count = _context.Produto.Count();
            var lst = _context.Produto.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            objretorno.lst = lst;
            objretorno.qntdRegistros = count;
            return objretorno;
        }

        public Produto CriarProduto(Produto produto)
        {
            try
            {
                if (produto != null)
                {
                    var count = _context.Produto.Count();
                    if (produto.Codigo == 0)
                    {
                        produto.Codigo = count + 1;
                    }
                    _context.Add(produto);
                    _context.SaveChanges();
                }
                else
                {
                    throw new Exception("Falha ao criar novo Produto");
                }
            }

            catch (Exception ex)
            {
                if (ex.InnerException is SqlException)
                {
                    var msg = ex.InnerException.Message.Substring(0, ex.InnerException.Message.IndexOf("\r"));
                    throw new Exception(msg, ex);
                }
                throw;
            }
            return produto;
        }

    }
}
