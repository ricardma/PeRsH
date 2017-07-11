using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrabFinal___PeRsH.Models
{
    //Representará os dados da tabela dos COMENTREPORTS
    public class ComentReports
    {
        [Key]
        public int ComRepId { get; set; }

        [Required]
        [Display(Name = "Razão do Report")]
        public string razRep { get; set; }

        [NotMapped]
        public bool visto { get; set; }

        //********************************
        //*CRIACAO DAS CHAVES FORASTEIRAS*
        //********************************
        //cria o atributo que irá funcionar como FK na BD
        //e relaciona-o com o atributo a seguir
        [ForeignKey("Comentarios")]
        [Display(Name = "Comentário da discussão a reportar")]
        public int ComentariosFK { get; set; }
        //Relaciona o objeto COMENTREPORT com um COMENTARIO
        public virtual Comentarios Comentarios { get; set; }

        //cria o atributo que irá funcionar como FK na BD
        //e relaciona-o com o atributo a seguir
        [ForeignKey("User")]
        [Display(Name = "User")]
        public string UtilizadorFK { get; set; }
        //Relaciona o objeto COMENTREPORT com um USER
        public virtual ApplicationUser User { get; set; }
    }
}