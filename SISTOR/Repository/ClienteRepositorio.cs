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
    public class ClienteRepositorio : IClienteRepositorio
    {
        private readonly AppDbContext _context;
        public ClienteRepositorio(AppDbContext context) 
        {
            _context = context;
        }

        public AppDbContext AppDbContext
        {
            get { return _context as AppDbContext; }
        }

        public Cliente CriarCliente(Cliente user)
        {
            if(user != null)
            {
                user.Pessoa.CPF = Md5Hash.CalculaHash(user.Pessoa.CPF);
                user.Pessoa.RG = Md5Hash.CalculaHash(user.Pessoa.RG);
                _context.Add(user);
                _context.SaveChanges();
            }
            return user;
        }

    }
}
