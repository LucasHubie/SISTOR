using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SISTOR.Models
{
    public class Cliente 
    {
        public int Id { get; set; }

        [Display(Name = "Pessoa")]
        [ForeignKey("Pessoa")]
        public int? IdPessoa { get; set; }
        public virtual Pessoa Pessoa { get; set; }
    }
}
