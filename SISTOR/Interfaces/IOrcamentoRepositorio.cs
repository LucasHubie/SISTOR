using SISTOR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SISTOR.Interfaces
{
    public interface IOrcamentoRepositorio
    {
        Orcamento CriarOrcamento(Orcamento user);
    }
}
