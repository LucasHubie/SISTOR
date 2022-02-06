using SISTOR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SISTOR.Interfaces
{
    public interface IClienteRepositorio
    {
        Cliente CriarCliente(Cliente cliente);

        List<Cliente> GetClientes();

        Cliente GetClienteByCPF(string cpf);

        Cliente GetClienteByNome(string cpf);

        Cliente GetClienteById(int id);

        Cliente AtualizarCliente(Cliente cliente);

    }
}
