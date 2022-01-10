using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SISTOR.Models
{
    public class UF
    {
        public int Id { get; set; }

        [Display(Name = "Estado")]
        [MaxLength(200)]
        public string EstadoNome { get; set; }

        [Display(Name = "Sigla")]
        [MaxLength(5)]
        public string EstadoSigla { get; set; }

    }
}
