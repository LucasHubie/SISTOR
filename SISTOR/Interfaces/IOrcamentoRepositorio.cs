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

        public void Delete(int id);

        public List<Orcamento> GetOrcamentos();

        public OrcamentoVM GetOrcamentoById(int id);
    }
}
