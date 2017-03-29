using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrabFinal___PeRsH.Models
{
    //Representará os dados da tabela dos COMENTREPORTS
    public class Reports
    {
        [Key]
        public int RepId { get; set; }

        [Required]
        [Display(Name = "Razão do Report")]
        public string razRep { get; set; }

        //********************************
        //*CRIACAO DAS CHAVES FORASTEIRAS*
        //********************************
        //cria o atributo que irá funcionar como FK na BD
        //e relaciona-o com o atributo a seguir
        [ForeignKey("Discussoes")]
        [Display(Name = "Discussão a reportar")]
        public int DiscussaoFK { get; set; }
        //Relaciona o objeto REPORT com uma DISCUSSAO
        public virtual Discussoes Discussoes { get; set; }

        //cria o atributo que irá funcionar como FK na BD
        //e relaciona-o com o atributo a seguir
        [ForeignKey("User")]
        [Display(Name = "User")]
        public string UtilizadorFK { get; set; }
        //Relaciona o objeto REPORT com um USER
        public virtual ApplicationUser User { get; set; }
    }
}