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
        public class retornoProdutos
        {
            public List<Produto> lst;

            public int qntdRegistros;
        }
    }
}
