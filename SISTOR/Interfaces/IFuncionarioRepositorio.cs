using SISTOR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SISTOR.Interfaces
{
    public interface IFuncionarioRepositorio
    {
        Funcionario CriarFuncionario(Funcionario funcionario);
        Funcionario EditarFuncionario(Funcionario funcionario);
        Funcionario GetFuncionarioById(int id);
        void ExcluirFuncionario(int id);
        Pessoa GetPessoa(int? id);
        List<Pessoa> GetFuncionario();
        List<Funcionario> GetFuncionarios();
        List<Funcionario> BuscarFuncionario(string busca);
        retornoFuncionarios GetFuncionarios(int pageNumber, int pageSize);
        int GetTotalFuncionario();
        public class retornoFuncionarios
        {
            public List<Funcionario> lst;

            public int qntdRegistros;
        }
    }
}
