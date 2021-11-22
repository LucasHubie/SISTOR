using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SISTOR.ViewModels
{
    public class UsuarioVM
    {
        [Required]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Required]
        [Display(Name ="Login")]
        public string Login { get; set; }

        [Required]
        [Display(Name = "Senha")]
        public string Senha { get; set; }
    }
}
