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
    public class FuncionarioRepositorio : IFuncionarioRepositorio
    {
        private readonly AppDbContext _context;

        public FuncionarioRepositorio(AppDbContext context)
        {
            _context = context;
        }

        public Funcionario CriarFuncionario(Funcionario funcionario)
        {
            try
            {
                _context.Add(funcionario);
                if (funcionario != null)
                {
                    _context.SaveChanges();
                }
                else
                {
                    throw new Exception("Falha ao criar novo Funcionário");
                }
            }

            catch (Exception ex)
            {
                if (ex.InnerException is SqlException)
                {
                    var msg = ex.InnerException.Message.Substring(0, ex.InnerException.Message.IndexOf("\r"));
                    throw new Exception(msg, ex);
                }
                throw new Exception("Falha ao criar novo Funcionário", ex);
            }
            return funcionario;
        }

        public Funcionario Atualizarfuncionario(Funcionario funcionario)
        {
            try
            {
                _context.Update(funcionario);
                if (funcionario != null)
                {
                    _context.SaveChanges();
                }
                else
                {
                    throw new Exception("Falha ao atualizar funcionário");
                }
            }

            catch (Exception ex)
            {
                if (ex.InnerException is SqlException)
                {
                    var msg = ex.InnerException.Message.Substring(0, ex.InnerException.Message.IndexOf("\r"));
                    throw new Exception(msg, ex);
                }
                throw new Exception("Falha ao atualizar funcionário", ex);
            }
            return funcionario;
        }

        public void Delete(int id)
        {
            try
            {
                //funcionario funcionario = new funcionario() { Id = id };
                var funcionario = GetFuncionarioById( id);
                if (funcionario != null)
                {
                    Pessoa pessoa = new Pessoa();
                    pessoa = GetPessoa(funcionario.IdPessoa);
                    _context.Remove(pessoa);
                    _context.Remove(funcionario);
                    _context.SaveChanges();
                }
                else
                {
                    throw new Exception("Funcionário não encontrado");
                }
                
            }

            catch (Exception ex)
            {
                if (ex.InnerException is SqlException)
                {
                    var msg = ex.InnerException.Message.Substring(0, ex.InnerException.Message.IndexOf("\r"));
                    throw new Exception(msg, ex);
                }
                throw new Exception("Falha ao criar novo funcionario", ex);
            }
        }

        //public Pessoa CriarFuncionario(Pessoa funcionario)
        //{
        //    if (funcionario != null)
        //    {
        //        funcionario.CPF = Md5Hash.CalculaHash(funcionario.CPF);
        //        funcionario.RG = Md5Hash.CalculaHash(funcionario.RG);
        //        _context.Add(funcionario);
        //        _context.SaveChanges();
        //    }
        //    Funcionario func = new Funcionario();
        //    //func.IdPessoa = GetFuncionarioId(funcionario.Id);
        //    func.IdPessoa = funcionario.Id;
        //    _context.Add(func);
        //    _context.SaveChanges();
        //    return funcionario;
        //}

        public Pessoa GetPessoa(int? id)
        {
            return _context.Pessoa.Where(x => x.Id == id).FirstOrDefault();
        }

        public List<Pessoa> GetFuncionario()
        {
            return _context.Pessoa.ToList();
        }

        public List<Funcionario> GetFuncionarios()
        {
            return _context.Funcionario.Include(x => x.Pessoa).ToList();
        }

        public Funcionario GetFuncionarioById(int id)
        {
            return _context.Funcionario.Where(p => p.Id == id).Include(p => p.Pessoa).FirstOrDefault();
        }

        public List<Funcionario> buscaFuncionario(string busca)
        {
            return _context.Funcionario.Where(x => x.Pessoa.Nome.Contains(busca) || x.Pessoa.Email.Contains(busca) || x.Pessoa.CPF.Contains(busca)
            || x.Pessoa.RG.Contains(busca)).Include(prop => prop.Pessoa).ToList();
        }

    }
}
