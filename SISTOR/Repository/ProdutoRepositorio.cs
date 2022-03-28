using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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
        private IOrcamentoRepositorio _orcamentoRepositorio;
        public ProdutoRepositorio(AppDbContext context, IOrcamentoRepositorio orcamentoRepositorio)
        {
            _context = context;
            _orcamentoRepositorio = orcamentoRepositorio;
        }
        
        public List<Produto> GetProduto()
        {
            return _context.Produto.ToList();
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
                    Produto prod = GetProdutoByCodigo(produto.Codigo);
                    if (prod != null && prod.Codigo == produto.Codigo)
                    {
                        throw new Exception("Falha ao criar produto! Código já existe!");
                    }
                    else
                    {
                        var count = _context.Produto.Count();
                        if (produto.Codigo == 0)
                        {
                            produto.Codigo = count + 1;
                        }
                        _context.Add(produto);
                        _context.SaveChanges();
                    }
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

        public Produto EditarProduto(Produto produto)
        {
            try
            {
                if (produto != null)
                {
                    Produto prod = GetProdutoByCodigo(produto.Codigo);
                    if (prod != null && prod.Codigo == produto.Codigo && prod.Id != produto.Id)
                    {
                        throw new Exception("Falha ao atualizar produto! Código já existe!");
                    } 
                    else
                    {
                        var count = _context.Produto.Count();
                        if (produto.Codigo == 0)
                        {
                            produto.Codigo = count + 1;
                        }
                        _context.Update(produto);
                        _context.SaveChanges();
                    }
                }
                else
                {
                    throw new Exception("Falha ao atualizar produto");
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

        public void ExcluirProduto(int id)
        {
            try
            {
                Produto produto = new Produto();
                produto = GetProdutoById(id);
                Itens item = new Itens();
                item = _orcamentoRepositorio.getItensOrcamentoByProduto(produto.Id);
                if (produto != null)
                {
                    if (item != null)
                    {
                        throw new Exception("Produto não pode ser apagado pois está vinculado a um orçamento!");
                    }
                    else
                    {
                        _context.Remove(produto);
                        _context.SaveChanges();
                    }
                }
                else
                {
                    throw new Exception("Produto não encontrado");
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
        }

        public List<Produto> BuscarProduto(string busca)
        {
            return _context.Produto.Where(x => x.Descricao.Contains(busca) || x.Codigo.ToString().Contains(busca)).ToList();
        }

        public retornoProdutos BuscaProduto(string busca, int pageNumber, int pageSize)
        {
            retornoProdutos objretorno = new retornoProdutos();
            var count = _context.Produto.Where(x => x.Descricao.Contains(busca) || x.Codigo.ToString().Contains(busca)).Count();
            var lst = _context.Produto.Where(x => x.Descricao.Contains(busca) || x.Codigo.ToString().Contains(busca)).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            objretorno.qntdRegistros = count;
            objretorno.lst = lst;
            return objretorno;
        }


        public Produto GetProdutoById(int id)
        {
            return _context.Produto.Where(p => p.Id == id).FirstOrDefault();
        }

        public Produto GetProdutoByCodigo(int codigo)
        {
            return _context.Produto.Where(p => p.Codigo == codigo).AsNoTracking().FirstOrDefault();
        }
    }
}
