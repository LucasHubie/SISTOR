using SISTOR.Models;
using SISTOR.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SISTOR.Interfaces
{
    public interface IOrcamentoRepositorio
    {
        Orcamento CriarOrcamento(OrcamentoVM orcamento);
        Orcamento UpdateOrcamento(OrcamentoVM orcamento);


       public void  CriarOrdemServico(OrdemServico os);
        public void Delete(int id);

        public List<Orcamento> GetOrcamentos();

        public retornoOrcamentos GetOrcamentos(int pageNumber, int pageSize);

        public List<OrdemServico> GetOrdensServico();
        

        public OrcamentoVM GetOrcamentoById(int id);
        public Itens getItensOrcamentoByProduto(int produto);

        public class retornoOrcamentos
        {
            public List<Orcamento> lst;

            public int qntdRegistros;
        }
    }
}
