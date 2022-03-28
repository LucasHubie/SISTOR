using SISTOR.Models;
using SISTOR.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SISTOR.ViewModels
{
    public class OrdemServicoVM
    {
        public OrdemServico ordemservico { get; set; }

        public Funcionario funcionario { get; set; }

        public List<ItensVM> lstprodutos { get; set; }

    }
}
