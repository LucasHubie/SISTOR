using SISTOR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SISTOR.Interfaces
{
    public interface IProdutoRepositorio
    {
        retornoProdutos GetProdutos(int pageNumber, int pageSize);
        Produto CriarProduto(Produto produto);
        List<Produto> BuscaProduto(string busca);

        retornoProdutos BuscaProduto(string busca, int pageNumber, int pageSize);
        Produto GetProdutoById(int id);
        Produto EditarProduto(Produto produto);
        void ExcluirProduto(int id);
        List<Produto> GetProduto();
        public class retornoProdutos
        {
            public List<Produto> lst;

            public int qntdRegistros;
        }
    }
}
