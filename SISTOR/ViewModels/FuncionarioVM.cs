using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SISTOR.ViewModels
{
    public class FuncionarioVM
    {
        [Required]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Required]
        [Display(Name = "RG")]
        public string RG { get; set; }

        [Required]
        [Display(Name = "CPF")]
        public string CPF { get; set; }

        [Display(Name = "Telefone")]
        public string Telefone { get; set; }

        [Display(Name = "Celular")]
        public string Celular { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Endereco")]
        public string Endereco { get; set; }


    }
}
