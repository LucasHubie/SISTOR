using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SISTOR.Configuration;
using SISTOR.Interfaces;
using SISTOR.Models;
using SISTOR.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SISTOR.Repository
{
    public class OrdemServicoRepositorio : IOrdemServicoRepositorio
    {
        private readonly AppDbContext _context;

        public OrdemServicoRepositorio(AppDbContext context)
        {
            _context = context;
        }
        public List<OrdemServico> GetOrdemServico()
        {
                return _context.OrdemServico.Include("Cliente.Pessoa").Include("Funcionario.Pessoa").Include("Orcamento").ToList();
        }

        public OrdemServico GetOrdemServioById(int id)
        {
            return _context.OrdemServico.Where(p => p.Id == id).FirstOrDefault();
        }
        public int GetOrdemAberta()
        {
            int count = _context.OrdemServico.Where(x => (int)x.Situacao == 1).Count();
            return count;
        }
        public int GetOrdemFechada()
        {
            int count = _context.OrdemServico.Where(x => (int)x.Situacao == 2).Count();
            return count;
        }
        public static string SemFormatacao(string Codigo)
        {
            return Codigo.Replace(".", string.Empty).Replace("-", string.Empty).Replace("/", string.Empty);
        }

        public OrdemServico CriarOrdemServico(OrdemServicoVM obj)
        {
            try
            {
                //  obj.cliente.Pessoa.CPF = Md5Hash.CalculaHash(obj.cliente.Pessoa.CPF);
                // obj.cliente.Pessoa.RG = Md5Hash.CalculaHash(obj.cliente.Pessoa.RG);


                obj.ordemservico.DataInclusao = DateTime.Now;
                _context.Add(obj.ordemservico);
                foreach (var itemloop in obj.lstprodutos)
                {
                    //Produto objProd = new Produto();
                    //objProd.Descricao = itemloop.Descricao;
                    //objProd.Valor = itemloop.ValorItem;
                    //objProd.TipoMedida = (Models.Enums.TipoMedida) 2;
                    //objProd.Codigo = 1;
                    //_context.Add(objProd);
                    Itens objItem = new Itens();
                    objItem.IdProduto = itemloop.idProduto;
                    objItem.OrdemServico = obj.ordemservico;
                    objItem.Quantidade = itemloop.Quantidade;
                    objItem.ValorItem = itemloop.ValorItem;
                    _context.Add(objItem);
                }
                _context.SaveChanges();
            }

            catch (Exception ex)
            {
                if (ex.InnerException is SqlException)
                {
                    var msg = ex.InnerException.Message.Substring(0, ex.InnerException.Message.IndexOf("\r"));
                    throw new Exception(msg, ex);
                }
                throw new Exception("Falha ao criar novo OrdemServico", ex);
            }
            return obj.ordemservico;
        }

        public OrdemServico EditarOrdemServico(OrdemServicoVM obj)
        {
            try
            {


                _context.Update(obj.ordemservico);

                var lst = _context.Itens.Include("Produto").Where(x => x.IdOrdemServico == obj.ordemservico.Id).ToList();

                foreach (var itemloop in obj.lstprodutos.Where(x => lst.Where(a => a.IdProduto == x.idProduto).Count() == 0))
                {
                    Itens objItem = new Itens();
                    objItem.IdProduto = itemloop.idProduto;
                    objItem.OrdemServico = obj.ordemservico;
                    objItem.Quantidade = itemloop.Quantidade;
                    objItem.ValorItem = itemloop.ValorItem;
                    _context.Add(objItem);
                }
                foreach (var itemloop in lst.Where(x => obj.lstprodutos.Where(a => a.idProduto == x.IdProduto).Count() == 0))
                {
                    _context.Remove(itemloop);
                }
                _context.SaveChanges();
            }

            catch (Exception ex)
            {
                if (ex.InnerException is SqlException)
                {
                    var msg = ex.InnerException.Message.Substring(0, ex.InnerException.Message.IndexOf("\r"));
                    throw new Exception(msg, ex);
                }
                throw new Exception("Falha ao alterar OrdemServico", ex);
            }
            return obj.ordemservico;
        }

        public void ExcluirOrdemServico(int id)
        {
            try
            {
                OrdemServico OrdemServico = new OrdemServico() { Id = id };
                _context.RemoveRange(_context.Itens.Where(x => x.IdOrdemServico == id));
                _context.Attach(OrdemServico);
                _context.Remove(OrdemServico);
                _context.SaveChanges();
            }

            catch (Exception ex)
            {
                if (ex.InnerException is SqlException)
                {
                    var msg = ex.InnerException.Message.Substring(0, ex.InnerException.Message.IndexOf("\r"));
                    throw new Exception(msg, ex);
                }
                throw new Exception("Falha ao criar novo Cliente", ex);
            }
        }
    }
}
