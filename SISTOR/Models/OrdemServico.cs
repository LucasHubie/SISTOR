using SISTOR.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SISTOR.Models
{
    public class OrdemServico
    {
        public int Id { get; set; }

        [Display(Name = "Data de Inclusão")]
        public DateTime DataInclusao { get; set; }

        [Display(Name = "Horas Trabalhadas")]
        [MaxLength(20)]
        public string HorasTrabalhadas { get; set; }

        [Display(Name = "Observações")]
        [MaxLength(300)]
        public string Observacoes { get; set; }

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

        [Display(Name = "Orçamento")]
        [ForeignKey("Orcamento")]
        public int? IdOrcamento { get; set; }
        public virtual Orcamento Orcamento { get; set; }

        [Display(Name = "Funcionário")]
        [ForeignKey("Funcionario")]
        public int? IdFuncionario { get; set; }
        public virtual Funcionario Funcionario { get; set; }
    }
}
