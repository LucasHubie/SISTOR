using SISTOR.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SISTOR.Models
{
    public class Orcamento
    {
        public int Id { get; set; }

        [Display(Name = "Tag de Identificação")]
        [MaxLength(50)]
        public string TagIdentificacao { get; set; }

        [Display(Name = "Data de Inclusão")]
        public DateTime DataInclusao { get; set; }

        [Display(Name = "Descrição")]
        [MaxLength(50)]
        public string Descricao { get; set; }

        [Display(Name = "Mão de Obra")]
        public decimal MaoDeObra { get; set; }

        [Display(Name = "Valor Total")]
        public decimal ValorTotal { get; set; }

        [Display(Name = "Situação")]
        public Situacao Situacao { get; set; }


        [Display(Name = "Cliente")]
        [ForeignKey("Cliente")]
        public int? IdCliente { get; set; }
        public virtual Cliente Cliente { get; set; }
    }
}
