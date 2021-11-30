using SISTOR.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SISTOR.Models
{
    public class Produto
    {
        public int Id { get; set; }

        [Display(Name = "Descrição")]
        [MaxLength(200)]
        public string Descricao { get; set; }

        [Display(Name = "Código")]
        public int Codigo { get; set; }

        [Display(Name = "Tipo de Medida")]
        public TipoMedida TipoMedida { get; set; }

        [Display(Name = "Valor")]
        public decimal Valor { get; set; }
    }
}
