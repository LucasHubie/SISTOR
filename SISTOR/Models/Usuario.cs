using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SISTOR.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Display(Name = "Login")]
        [MaxLength(100)]
        public string Login { get; set; }

        [Display(Name = "Senha")]
        public string Senha { get; set; }

        [Display(Name ="Pessoa")]
        [ForeignKey("Pessoa")]
        public int? IdPessoa { get; set; }
        public virtual Pessoa Pessoa { get; set; }
    }
}
