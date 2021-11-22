using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SISTOR.Models
{
    public class Itens
    {
        public int Id { get; set; }

        [Display(Name = "Quantidade")]
        public int Quantidade { get; set; }

        [Display(Name = "Valor do Item")]
        public decimal ValorItem { get; set; }

        [Display(Name ="Observações")]
        [MaxLength(200)]
        public string Observacoes { get; set; }

        [Display(Name = "Orçamento")]
        [ForeignKey("Orcamento")]
        public int? IdOrcamento { get; set; }
        public virtual Orcamento Orcamento { get; set; }

        [Display(Name = "Ordem de Serviço")]
        [ForeignKey("OrdemServico")]
        public int? IdOrdemServico { get; set; }
        public virtual OrdemServico OrdemServico { get; set; }

        [Display(Name = "Produto")]
        [ForeignKey("Produto")]
        public int? IdProduto { get; set; }
        public virtual Produto Produto { get; set; }
    }
}
