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

        public List<Cliente> GetClientes()
        {
            return _context.Cliente.Include(x => x.Pessoa).ToList();
        }

        public Cliente GetClienteByCPF(string cpf)
        {
            return _context.Cliente.Include(x => x.Pessoa).Where(b => b.Pessoa.CPF == cpf).FirstOrDefault();
        }

        public Cliente GetClienteByNome(string nome)
        {
            return _context.Cliente.Where(x => x.Pessoa.Nome.Contains(nome)).Include(prop => prop.Pessoa).FirstOrDefault();
        }

        
    }
}
