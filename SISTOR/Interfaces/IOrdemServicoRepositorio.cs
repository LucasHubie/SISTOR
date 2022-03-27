using SISTOR.Models;
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
    }
}
