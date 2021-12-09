using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SISTOR.Models
{
    public class Pessoa
    {
        public int Id { get; set; }

        [Display(Name = "Nome")]
        [MaxLength(200)]
        public string Nome { get; set; }

        [Display(Name = "RG")]
        [MaxLength(100)]
        public string RG { get; set; }

        [Display(Name = "CPF")]
        [MaxLength(100)]
        public string CPF { get; set; }

        [Display(Name = "CNPJ")]
        [MaxLength(14)]
        public string CNPJ { get; set; }

        [Display(Name = "Razão Social")]
        [MaxLength(200)]
        public string RazaoSocial { get; set; }

        [Display(Name = "Nome Fantasia")]
        [MaxLength(200)]
        public string NomeFantasia { get; set; }

        [Display(Name = "Telefone")]
        [MaxLength(20)]
        public string Telefone { get; set; }

        [Display(Name = "Celular")]
        [MaxLength(20)]
        public string Celular { get; set; }

        [Display(Name = "Email")]
        [MaxLength(100)]
        public string Email { get; set; }

        [Display(Name = "CEP")]
        [MaxLength(15)]
        public string CEP { get; set; }

        [Display(Name = "Número")]
        public int Numero { get; set; }

        [Display(Name = "Complemento")]
        [MaxLength(50)]
        public string Complemento { get; set; }

        [Display(Name = "Ponto de Referência")]
        [MaxLength(50)]
        public string Referencia { get; set; }

        [Display(Name = "Endereço")]
        [MaxLength(100)]
        public string Endereco { get; set; }

        [Display(Name = "Cidade")]
        [MaxLength(100)]
        public string Cidade { get; set; }

        [Display(Name = "UF")]
        public UF UF { get; set; }

        [Display(Name = "Tipo de Pessoa")]
        public TipoPessoa TipoPessoa { get; set; }

    }
}
