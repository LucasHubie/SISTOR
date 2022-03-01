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
        Funcionario Atualizarfuncionario(Funcionario funcionario);
        Funcionario GetFuncionarioById(int id);
        void Delete(int id);
        Pessoa GetPessoa(int? id);

        //int GetFuncionarioId(int? id);
        List<Pessoa> GetFuncionario();

        List<Funcionario> GetFuncionarios();
    }
}
