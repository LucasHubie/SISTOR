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
                _context.Add(cliente);
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
                throw new Exception("Falha ao criar novo Cliente", ex);
            }
            return cliente;
        }

        public Cliente AtualizarCliente(Cliente cliente)
        {
            try
            {
                _context.Update(cliente);
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
                throw new Exception("Falha ao atualizar Cliente", ex);
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
