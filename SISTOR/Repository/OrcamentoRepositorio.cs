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

        public static string SemFormatacao(string Codigo)
        {
            return Codigo.Replace(".", string.Empty).Replace("-", string.Empty).Replace("/", string.Empty);
        }


        public Orcamento CriarOrcamento(OrcamentoVM obj)
        {
            try
            {
                //  obj.cliente.Pessoa.CPF = Md5Hash.CalculaHash(obj.cliente.Pessoa.CPF);
                // obj.cliente.Pessoa.RG = Md5Hash.CalculaHash(obj.cliente.Pessoa.RG);
                Orcamento orc = new Orcamento();
                orc = GetOrcamentoByTag(obj.orcamento.TagIdentificacao);
                if (orc != null && orc.TagIdentificacao == obj.orcamento.TagIdentificacao)
                {
                    throw new Exception("Falha ao criar novo Orcamento! Tag de identificação já existe!");
                }
                else
                {
                    if (obj.cliente.Id != 0)
                    {
                        obj.orcamento.IdCliente = obj.cliente.Id;
                    }
                    else
                    {
                        obj.orcamento.Cliente = obj.cliente;
                    }

                    if (!String.IsNullOrEmpty(obj.orcamento.Cliente.Pessoa.CPF))
                    {
                        obj.orcamento.Cliente.Pessoa.CPF = SemFormatacao(obj.orcamento.Cliente.Pessoa.CPF);
                    }
                    if (!String.IsNullOrEmpty(obj.orcamento.Cliente.Pessoa.CNPJ))
                    {
                        obj.orcamento.Cliente.Pessoa.CNPJ = SemFormatacao(obj.orcamento.Cliente.Pessoa.CNPJ);
                    }

                    obj.orcamento.DataInclusao = DateTime.Now;
                    _context.Add(obj.orcamento);
                    foreach (var itemloop in obj.lstprodutos)
                    {

                        Itens objItem = new Itens();
                        objItem.IdProduto = itemloop.idProduto;
                        objItem.Orcamento = obj.orcamento;
                        objItem.Quantidade = itemloop.Quantidade;
                        objItem.ValorItem = itemloop.ValorItem;
                        _context.Add(objItem);
                    }
                    _context.SaveChanges();
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
            return obj.orcamento;
        }

        public Orcamento EditarOrcamento(OrcamentoVM obj)
        {
            try
            {
                Orcamento orc = new Orcamento();
                orc = GetOrcamentoByTag(obj.orcamento.TagIdentificacao);
                if (orc != null && orc.Id != obj.orcamento.Id && orc.TagIdentificacao == obj.orcamento.TagIdentificacao)
                {
                    throw new Exception("Falha ao alterar Orcamento! Tag de identificação já existe!");
                }
                else
                {
                    if (!String.IsNullOrEmpty(obj.orcamento.Cliente.Pessoa.CPF))
                    {
                        obj.orcamento.Cliente.Pessoa.CPF = SemFormatacao(obj.orcamento.Cliente.Pessoa.CPF);
                    }
                    if (!String.IsNullOrEmpty(obj.orcamento.Cliente.Pessoa.CNPJ))
                    {
                        obj.orcamento.Cliente.Pessoa.CNPJ = SemFormatacao(obj.orcamento.Cliente.Pessoa.CNPJ);
                    }
                    obj.orcamento.IdCliente = obj.cliente.Id;
                    obj.orcamento.Cliente = obj.cliente;

                    _context.Update(obj.orcamento);

                    var lst = _context.Itens.Include("Produto").Where(x => x.IdOrcamento == obj.orcamento.Id).ToList();

                    foreach (var itemloop in obj.lstprodutos.Where(x => lst.Where(a => a.IdProduto == x.idProduto).Count() == 0))
                    {
                        Itens objItem = new Itens();
                        objItem.IdProduto = itemloop.idProduto;
                        objItem.Orcamento = obj.orcamento;
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

        public int GetOrcamentoAprovado()
        {
            int count = _context.Orcamento.Where(x => (int)x.Situacao == 1).Count();
            return count;
        }

        public int GetOrcamentoAguardando()
        {
            int count = _context.Orcamento.Where(x => (int)x.Situacao == 3).Count();
            return count;
        }
        public List<Itens> GetItensOrcamento()
        {
            return _context.Itens.Include("Produto").ToList();
        }

        public List<Itens> GetItensOrcamentoById(int id)
        {
            return _context.Itens.Include("Produto").Where(x => x.IdOrcamento == id).ToList();
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
                objitens.idProduto = itemloop.Produto.Id;
                objitens.Descricao = itemloop.Produto.Descricao;
                objitens.ValorItem = itemloop.Produto.Valor;
                objitens.Quantidade = itemloop.Quantidade;
                obj.lstprodutos.Add(objitens);
            }
            return obj;

        }

        public void ExcluirOrcamento(int id)
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

        public retornoOrcamentos BuscarOrcamento(string busca, int pageNumber, int pageSize)
        {
            retornoOrcamentos objretorno = new retornoOrcamentos();
            var count = _context.Orcamento.Where(x => x.Cliente.Pessoa.Nome.Contains(busca) || x.TagIdentificacao.Contains(busca)).Include(prop => prop.Cliente.Pessoa).Count();
            var lst = _context.Orcamento.Where(x => x.Cliente.Pessoa.Nome.Contains(busca) || x.TagIdentificacao.Contains(busca)).Include(prop => prop.Cliente.Pessoa).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            objretorno.qntdRegistros = count;
            objretorno.lst = lst;
            return objretorno;
        }


        public Itens getItensOrcamentoByProduto(int produto)
        {
            return _context.Itens.Include("Produto").Where(r => r.IdProduto == produto).FirstOrDefault();
        }

        public Orcamento GetOrcamentoByTag(string tag)
        {
            return _context.Orcamento.Where(x => x.TagIdentificacao == tag).AsNoTracking().FirstOrDefault();
        }
    }
}
