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

        List<Cliente> GetClienteByNome(string cpf);

        Cliente GetClienteById(int id);

        Cliente AtualizarCliente(Cliente cliente);

        List<Pessoa> GetPessoaCliente();

        void Delete(int id);
        Pessoa GetPessoa(int? id);
        retornoClientes buscaCliente(string busca, int pageNumber, int pageSize);

        retornoClientes buscaClienteReduzido(string busca, int pageNumber, int pageSize);

        

        List<Cliente> buscaCliente(string busca);
        retornoClientes GetClientes(int pageNumber, int pageSize);

        public class retornoClientes
        {
            public List<Cliente> lst;

            public int qntdRegistros;
        }

    }
}
