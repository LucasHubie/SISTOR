using SISTOR.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SISTOR.ViewModels
{
    public class ClienteVM
    {
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Display(Name = "RG")]
        public string RG { get; set; }

        [Display(Name = "CPF")]
        public string CPF { get; set; }

        [Display(Name = "Nome Fantasia")]
        public string NomeFantasia { get; set; }

        [Display(Name = "Razão Social")]
        public string RazaoSocial { get; set; }

        [Display(Name = "Telefone")]
        public string Telefone { get; set; }

        [Display(Name = "Celular")]
        public string Celular { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Endereço")]
        public string Endereco { get; set; }

        [Required]
        [Display(Name = "CEP")]
        public string CEP { get; set; }

        [Required]
        [Display(Name = "Numero")]
        public int Numero { get; set; }

        [Display(Name = "Complemento")]
        public string Complemento { get; set; }

        [Display(Name = "Ponto de Referência")]
        public string Referencia { get; set; }

        [Required]
        [Display(Name = "Tipo de Pessoa")]
        public TipoPessoa TipoPessoa { get; set; }
    }
}
