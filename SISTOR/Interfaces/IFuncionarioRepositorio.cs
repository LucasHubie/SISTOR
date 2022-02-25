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

        //int GetFuncionarioId(int? id);
        List<Pessoa> GetFuncionario();

        List<Funcionario> GetFuncionarios();
    }
}
