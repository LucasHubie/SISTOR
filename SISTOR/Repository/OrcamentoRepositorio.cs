using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PagedList;
using SISTOR.Configuration;
using SISTOR.Controllers;
using SISTOR.Interfaces;
using SISTOR.Models;
using SISTOR.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static SISTOR.Interfaces.IOrcamentoRepositorio;

namespace SISTOR.Repository
{
    public class OrcamentoRepositorio : IOrcamentoRepositorio
    {
        private readonly AppDbContext _context;
        public OrcamentoRepositorio(AppDbContext context)
        {
            _context = context;
        }


        public Orcamento CriarOrcamento(OrcamentoVM obj)
        {
            try
            {
                //  obj.cliente.Pessoa.CPF = Md5Hash.CalculaHash(obj.cliente.Pessoa.CPF);
                // obj.cliente.Pessoa.RG = Md5Hash.CalculaHash(obj.cliente.Pessoa.RG);
                if (obj.cliente.Id != 0)
                {
                    obj.orcamento.IdCliente = obj.cliente.Id;
                }
                else
                {
                    obj.orcamento.Cliente = obj.cliente;
                }

                obj.orcamento.DataInclusao = DateTime.Now;
                _context.Add(obj.orcamento);
                foreach (var itemloop in obj.lstprodutos)
                {
                    Produto objProd = new Produto();
                    objProd.Descricao = itemloop.Descricao;
                    objProd.Valor = itemloop.ValorItem;
                    objProd.TipoMedida = (Models.Enums.TipoMedida) 2;
                    objProd.Codigo = 1;
                    _context.Add(objProd);
                    Itens objItem = new Itens();
                    objItem.Produto = objProd;
                    objItem.Orcamento = obj.orcamento;
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
                throw new Exception("Falha ao criar novo Orcamento", ex);
            }
            return obj.orcamento;
        }

        public Orcamento UpdateOrcamento(OrcamentoVM obj)
        {
            try
            {

                _context.Update(obj.orcamento);
               /* foreach (var itemloop in obj.lstprodutos)
                {
                    if(itemloop.idItem == 0)
                    {
                        Produto objProd = new Produto();
                        objProd.Descricao = itemloop.Descricao;
                        objProd.Valor = itemloop.ValorItem;
                        objProd.TipoMedida = (Models.Enums.TipoMedida)2;
                        objProd.Codigo = 1;
                        _context.Add(objProd);
                        Itens objItem = new Itens();
                        objItem.Produto = objProd;
                        objItem.Orcamento = obj.orcamento;
                        objItem.Quantidade = itemloop.Quantidade;
                        objItem.ValorItem = itemloop.ValorItem;
                        _context.Add(objItem);
                    }  
                }*/
                _context.SaveChanges();
            }

            catch (Exception ex)
            {
                if (ex.InnerException is SqlException)
                {
                    var msg = ex.InnerException.Message.Substring(0, ex.InnerException.Message.IndexOf("\r"));
                    throw new Exception(msg, ex);
                }
                throw new Exception("Falha ao alterar Orcamento", ex);
            }
            return obj.orcamento;
        }

        public List<Orcamento> GetOrcamentos()
        {
            return _context.Orcamento.Include("Cliente.Pessoa").ToList();
        }

        public retornoOrcamentos GetOrcamentos(int pageNumber, int pageSize)
        {
            retornoOrcamentos objretorno = new retornoOrcamentos();
            var count = _context.Orcamento.Count();
            var lst = _context.Orcamento.Include("Cliente.Pessoa").Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            objretorno.lst = lst;
            objretorno.qntdRegistros = count;
            return objretorno;
        }


        public List<OrdemServico> GetOrdensServico()
        {
            return _context.OrdemServico.Include("Cliente.Pessoa").Include("Funcionario.Pessoa").Include("Orcamento").ToList();
        }

        public OrcamentoVM GetOrcamentoById(int id)
        {
            OrcamentoVM obj = new OrcamentoVM();
            obj.orcamento = _context.Orcamento.Include("Cliente.Pessoa").FirstOrDefault(u => u.Id == id);
            var lst = _context.Itens.Include("Produto").Where(r => r.IdOrcamento == id).ToList();
            if(lst.Count > 0)
            {
                obj.lstprodutos = new List<ItensVM>();
            }
            foreach (var itemloop in lst)
            {
                ItensVM objitens = new ItensVM();
                objitens.idItem = itemloop.Id;
                objitens.Descricao = itemloop.Produto.Descricao;
                objitens.ValorItem = itemloop.Produto.Valor;
                objitens.Quantidade = itemloop.Quantidade;
                obj.lstprodutos.Add(objitens);
            }
            return obj;

        }

        public void Delete(int id)
        {
            try
            {
                Orcamento orcamento = new Orcamento() { Id = id };
                _context.RemoveRange(_context.Itens.Where(x => x.IdOrcamento == id));
                _context.Attach(orcamento);
                _context.Remove(orcamento);
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

        public void CriarOrdemServico(OrdemServico os)
        {
            try
            {
                _context.Add(os);
                _context.SaveChanges();
            }

            catch (Exception ex)
            {
                if (ex.InnerException is SqlException)
                {
                    var msg = ex.InnerException.Message.Substring(0, ex.InnerException.Message.IndexOf("\r"));
                    throw new Exception(msg, ex);
                }
                throw new Exception("Falha ao criar Ordem Servico", ex);
            }
         
        }
    }
}
