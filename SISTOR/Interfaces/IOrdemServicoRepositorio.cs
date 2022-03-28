using SISTOR.Models;
using SISTOR.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SISTOR.Interfaces
{
    public interface IOrdemServicoRepositorio
    {
        List<OrdemServico> GetOrdemServico();
        OrdemServico GetOrdemServioById(int id);
        int GetOrdemAberta();
        int GetOrdemFechada();

        OrdemServico CriarOrdemServico(OrdemServicoVM OrdemServico);
        OrdemServico UpdateOrdemServico(OrdemServicoVM OrdemServico);
        public void Delete(int id);

        public class retornoOrdemservico
        {
            public List<OrdemServico> lst;

            public int qntdRegistros;
        }
    }
}
