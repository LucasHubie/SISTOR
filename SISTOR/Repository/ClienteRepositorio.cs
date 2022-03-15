using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SISTOR.Configuration;
using SISTOR.Controllers;
using SISTOR.Interfaces;
using SISTOR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static SISTOR.Interfaces.IClienteRepositorio;

namespace SISTOR.Repository
{
    public class ClienteRepositorio : IClienteRepositorio
    {
        private readonly AppDbContext _context;
        public ClienteRepositorio(AppDbContext context)
        {
            _context = context;
        }

        public Cliente CriarCliente(Cliente cliente)
        {
            try
            {
                //cliente.Pessoa.CPF = Md5Hash.CalculaHash(cliente.Pessoa.CPF);
                //cliente.Pessoa.RG = Md5Hash.CalculaHash(cliente.Pessoa.RG);
                Cliente cpfDuplicado = new Cliente();
                Cliente rgDuplicado = new Cliente();
                Cliente cnpjDuplicado = new Cliente();
                cpfDuplicado = GetClienteByCPF(cliente.Pessoa.CPF);
                rgDuplicado = GetClienteByRG(cliente.Pessoa.RG);
                cnpjDuplicado = GetClienteByCNPJ(cliente.Pessoa.CNPJ);
                if (cpfDuplicado != null && cpfDuplicado.Pessoa.CPF != "" && cpfDuplicado.Pessoa.CPF == cliente.Pessoa.CPF)
                {
                    throw new Exception("Falha ao criar novo Cliente! CPF já cadastrado!");
                }
                else if (rgDuplicado != null && rgDuplicado.Pessoa.RG != "" && rgDuplicado.Pessoa.RG == cliente.Pessoa.RG)
                {
                    throw new Exception("Falha ao criar novo Cliente! RG já cadastrado!");
                } 
                else if (cnpjDuplicado != null && cnpjDuplicado.Pessoa.CNPJ != "" && cnpjDuplicado.Pessoa.CNPJ == cliente.Pessoa.CNPJ)
                {
                    throw new Exception("Falha ao criar novo Cliente! CNPJ já cadastrado!");
                }
                else
                {
                    _context.Add(cliente);
                }
                
                if(cliente != null)
                {
                    _context.SaveChanges();
                }
                else
                {
                    throw new Exception("Falha ao criar novo Cliente");
                }
            }

            catch (Exception ex ){
                if (ex.InnerException is SqlException)
                {
                    var msg = ex.InnerException.Message.Substring(0, ex.InnerException.Message.IndexOf("\r"));
                    throw new Exception(msg, ex);
                }
                //throw new Exception("Falha ao criar novo Cliente", ex);
                throw;
            }
            return cliente;
        }

        public Cliente AtualizarCliente(Cliente cliente)
        {
            try
            {
                Cliente cpfDuplicado = new Cliente();
                Cliente rgDuplicado = new Cliente();
                Cliente cnpjDuplicado = new Cliente();
                cpfDuplicado = GetClienteByCPF(cliente.Pessoa.CPF);
                rgDuplicado = GetClienteByRG(cliente.Pessoa.RG);
                cnpjDuplicado = GetClienteByCNPJ(cliente.Pessoa.CNPJ);
                if (cpfDuplicado != null && cpfDuplicado.Pessoa.CPF != "" && cpfDuplicado.Id != cliente.Id && cpfDuplicado.Pessoa.CPF == cliente.Pessoa.CPF)
                {
                    throw new Exception("Falha ao atualizar Cliente! CPF já cadastrado!");
                }
                else if (rgDuplicado != null && rgDuplicado.Pessoa.RG != "" && rgDuplicado.Id != cliente.Id && rgDuplicado.Pessoa.RG == cliente.Pessoa.RG)
                {
                    throw new Exception("Falha ao atualizar Cliente! RG já cadastrado!");
                }
                else if (cnpjDuplicado != null && cnpjDuplicado.Pessoa.CNPJ != "" && cnpjDuplicado.Pessoa.CNPJ == cliente.Pessoa.CNPJ)
                {
                    throw new Exception("Falha ao atualizar Cliente! CNPJ já cadastrado!");
                }
                else
                {
                    _context.Update(cliente);
                }
                
                if (cliente != null)
                {
                    _context.SaveChanges();
                }
                else
                {
                    throw new Exception("Falha ao atualizar Cliente");
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
            return cliente;
        }

        public void Delete(int id)
        {
            try
            {
                //Cliente cliente = new Cliente() { Id = id };
                var cliente = GetClienteById( id);
                if (cliente != null)
                {
                    Pessoa pessoa = new Pessoa();
                    pessoa = GetPessoa(cliente.IdPessoa);
                    _context.Remove(pessoa);
                    _context.Remove(cliente);
                    _context.SaveChanges();
                }
                else
                {
                    throw new Exception("Cliente não encontrado");
                }
                
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

        public Pessoa GetPessoa(int? id)
        {
            return _context.Pessoa.Where(x => x.Id == id).FirstOrDefault();
        }
        public List<Cliente> GetClientes()
        {
            return _context.Cliente.Include(x => x.Pessoa).ToList();
        }

        public retornoClientes GetClientes(int pageNumber, int pageSize)
        {
            retornoClientes objretorno = new retornoClientes();
            var count = _context.Cliente.Count();
            var lst = _context.Cliente.Include(x => x.Pessoa).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            objretorno.lst = lst;
            objretorno.qntdRegistros = count;
            return objretorno;
        }

        public List<Pessoa> GetPessoaCliente()
        {
            return _context.Pessoa.ToList();
        }

        public Cliente GetClienteById(int id)
        {
            return _context.Cliente.Where(p => p.Id == id).Include(p => p.Pessoa).FirstOrDefault();
        }

        public Cliente GetClienteByCPF(string cpf)
        {
            return _context.Cliente.Include(x => x.Pessoa).Where(b => b.Pessoa.CPF == cpf).FirstOrDefault();
        }

        public Cliente GetClienteByRG(string rg)
        {
            return _context.Cliente.Include(x => x.Pessoa).Where(b => b.Pessoa.RG == rg).FirstOrDefault();
        }

        public Cliente GetClienteByCNPJ(string cnpj)
        {
            return _context.Cliente.Include(x => x.Pessoa).Where(b => b.Pessoa.CNPJ == cnpj).FirstOrDefault();
        }

        public Cliente GetClienteByNome(string nome)
        {
            return _context.Cliente.Where(x => x.Pessoa.Nome.Contains(nome)).Include(prop => prop.Pessoa).FirstOrDefault();
        }

        public List<Cliente> buscaCliente(string busca)
        {
            //return _context.Cliente.Include(x => x.Pessoa.Nome.Contains(busca) || x.Pessoa.NomeFantasia.Contains(busca)).OrderBy(x => x.Pessoa.Nome).ToList();
            return _context.Cliente.Where(x => x.Pessoa.Nome.Contains(busca) || x.Pessoa.NomeFantasia.Contains(busca) || x.Pessoa.Email.Contains(busca) || x.Pessoa.CPF.Contains(busca)
            || x.Pessoa.RG.Contains(busca) || x.Pessoa.CNPJ.Contains(busca) || x.Pessoa.RazaoSocial.Contains(busca))
                .Include(prop => prop.Pessoa).ToList();
        }

       

    }
}
