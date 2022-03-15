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
using static SISTOR.Interfaces.IFuncionarioRepositorio;

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
                Funcionario cpfDuplicado = new Funcionario();
                Funcionario rgDuplicado = new Funcionario();
                cpfDuplicado = GetFuncionarioByCPF(funcionario.Pessoa.CPF);
                rgDuplicado = GetFuncionarioByRG(funcionario.Pessoa.RG);
                if (cpfDuplicado != null && cpfDuplicado.Pessoa.CPF != "" && cpfDuplicado.Pessoa.CPF == funcionario.Pessoa.CPF)
                {
                    throw new Exception("Falha ao criar novo Funcionário! CPF já cadastrado!");
                }
                else if (rgDuplicado != null && rgDuplicado.Pessoa.RG != "" && rgDuplicado.Pessoa.RG == funcionario.Pessoa.RG)
                {
                    throw new Exception("Falha ao criar novo Funcionário! RG já cadastrado!");
                }
                else
                {
                    _context.Add(funcionario);
                }

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
                throw;
            }
            return funcionario;
        }

        public Funcionario Atualizarfuncionario(Funcionario funcionario)
        {
            try
            {
                Funcionario cpfDuplicado = new Funcionario();
                Funcionario rgDuplicado = new Funcionario();
                cpfDuplicado = GetFuncionarioByCPF(funcionario.Pessoa.CPF);
                rgDuplicado = GetFuncionarioByRG(funcionario.Pessoa.RG);
                if (cpfDuplicado != null && cpfDuplicado.Pessoa.CPF != "" && cpfDuplicado.Id != funcionario.Id && cpfDuplicado.Pessoa.CPF == funcionario.Pessoa.CPF)
                {
                    throw new Exception("Falha ao atualizar Funcionário! CPF já cadastrado!");
                }
                else if (rgDuplicado != null && rgDuplicado.Pessoa.RG != "" && rgDuplicado.Id != funcionario.Id && rgDuplicado.Pessoa.RG == funcionario.Pessoa.RG)
                {
                    throw new Exception("Falha ao atualizar Funcionário! RG já cadastrado!");
                }
                else
                {
                    _context.Update(funcionario);
                }
                
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
                throw;
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

        public retornoFuncionarios GetFuncionarios(int pageNumber, int pageSize)
        {
            retornoFuncionarios objretorno = new retornoFuncionarios();
            var count = _context.Funcionario.Count();
            var lst = _context.Funcionario.Include(x => x.Pessoa).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            objretorno.lst = lst;
            objretorno.qntdRegistros = count;
            return objretorno;
        }

        public Funcionario GetFuncionarioByCPF(string cpf)
        {
            return _context.Funcionario.Include(x => x.Pessoa).AsNoTracking().Where(b => b.Pessoa.CPF == cpf).FirstOrDefault();
        }

        public Funcionario GetFuncionarioByRG(string rg)
        {
            return _context.Funcionario.Include(x => x.Pessoa).AsNoTracking().Where(b => b.Pessoa.RG == rg).FirstOrDefault();
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
