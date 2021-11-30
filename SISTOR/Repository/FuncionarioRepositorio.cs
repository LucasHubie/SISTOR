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
    public class FuncionarioRepositorio : IFuncionarioRepositorio
    {
        private readonly AppDbContext _context;

        public FuncionarioRepositorio(AppDbContext context)
        {
            _context = context;
        }

        public Pessoa CriarFuncionario(Pessoa funcionario)
        {
            if (funcionario != null)
            {
                funcionario.CPF = Md5Hash.CalculaHash(funcionario.CPF);
                funcionario.RG = Md5Hash.CalculaHash(funcionario.RG);
                _context.Add(funcionario);
                _context.SaveChanges();
            }
            Funcionario func = new Funcionario();
            //func.IdPessoa = GetFuncionarioId(funcionario.Id);
            func.IdPessoa = funcionario.Id;
            _context.Add(func);
            _context.SaveChanges();
            return funcionario;
        }

        public List<Pessoa> GetFuncionario()
        {
            return _context.Pessoa.ToList();
        }

        public List<Funcionario> GetFuncionarios()
        {
            return _context.Funcionario.Include(x => x.Pessoa).ToList();
        }

    }
}
