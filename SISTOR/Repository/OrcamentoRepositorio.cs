using Microsoft.EntityFrameworkCore;
using SISTOR.Configuration;
using SISTOR.Controllers;
using SISTOR.Interfaces;
using SISTOR.Models;
using SISTOR.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                throw new Exception("Falha ao criar novo Cliente", ex);
            }
            return obj.orcamento;
        }

        public Orcamento UpdateOrcamento(OrcamentoVM obj)
        {
            try
            {

                obj.orcamento.DataInclusao = DateTime.Now;
                _context.Update(obj.orcamento);
                foreach (var itemloop in obj.lstprodutos)
                {
                    Produto objProd = new Produto();
                    objProd.Descricao = itemloop.Descricao;
                    objProd.Valor = itemloop.ValorItem;
                    objProd.TipoMedida = (Models.Enums.TipoMedida)2;
                    objProd.Codigo = 1;
                    _context.Update(objProd);
                    Itens objItem = new Itens();
                    objItem.Produto = objProd;
                    objItem.Orcamento = obj.orcamento;
                    objItem.Quantidade = itemloop.Quantidade;
                    objItem.ValorItem = itemloop.ValorItem;
                    _context.Update(objItem);
                }
                _context.SaveChanges();
            }

            catch (Exception ex)
            {
                throw new Exception("Falha ao criar novo Cliente", ex);
            }
            return obj.orcamento;
        }

        public List<Orcamento> GetOrcamentos()
        {
            return _context.Orcamento.Include("Cliente.Pessoa").ToList();
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
                _context.Attach(orcamento);
                _context.Remove(orcamento);
                _context.SaveChanges();
            }

            catch (Exception ex)
            {
                throw new Exception("Falha ao criar novo Cliente", ex);
            }
        }
    }
}
