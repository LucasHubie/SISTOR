using SISTOR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SISTOR.Interfaces
{
    public interface IFuncionarioRepositorio
    {
        Pessoa CriarFuncionario(Pessoa funcionario);
        //int GetFuncionarioId(int? id);
        List<Pessoa> GetFuncionario();
    }
}
