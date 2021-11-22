using SISTOR.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SISTOR.ViewModels
{
    public class ProdutoVM
    {
        [Required]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Required]
        [Display(Name = "Código")]
        public int Codigo { get; set; }

        [Required]
        [Display(Name = "Valor")]
        public decimal Valor { get; set; }

        [Required]
        [Display(Name = "Tipo de Medida")]
        public TipoMedida TipoMedida { get; set; }
    }
}
