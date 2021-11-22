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
        [Display(Name = "Data de Inclusão")]
        public DateTime DataInclusao { get; set; }

        [Display(Name = "Horas Trabalhadas")]
        public string HorasTrabalhadas { get; set; }

        [Display(Name = "Observações")]
        public string Observacoes { get; set; }

        [Display(Name = "Mão de Obra")]
        public decimal MaoDeObra { get; set; }

        [Display(Name = "Valor Total")]
        public decimal ValorTotal { get; set; }

        [Display(Name = "Nome do Funcionário")]
        public string NomeFuncionario { get; set; }

        [Display(Name = "Nome do Cliente")]
        public string NomeCliente { get; set; }

        [Display(Name = "Situação")]
        public Situacao Situacao { get; set; }

    }
}
