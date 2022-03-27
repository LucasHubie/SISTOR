using SISTOR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SISTOR.Interfaces
{
    public interface IOrdemServicoRepositorio
    {
        OrdemServico GetOrdemServioById(int id);
    }
}
