using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SISTOR.ViewModels
{
    public class ItensVM
    {
        public int idItem { get; set; }

        public string Descricao { get; set; }

        public int Quantidade { get; set; }

        public decimal ValorItem { get; set; }
    }
}
