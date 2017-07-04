using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrabFinal___PeRsH.Models
{
    public class Dislikes
    {
        //Representará os dados da tabela DISLIKES
        [Key]
        public int dislikeID { get; set; }

        [Column(TypeName = "date")]
        public DateTime dataLike { get; set; }

        //********************************
        //*CRIACAO DAS CHAVES FORASTEIRAS*
        //********************************
        //Relaciona o objeto DISLIKES com uma DISCUSSAO
        public virtual Discussoes Discussoes { get; set; }
        //cria o atributo que irá funcionar como FK na BD
        //e relaciona-o com o atributo a seguir
        [ForeignKey("Discussoes")]
        [Display(Name = "Discussão")]
        public int DiscussaoFK { get; set; }

        //cria o atributo que irá funcionar como FK na BD
        //e relaciona-o com o atributo a seguir
        [ForeignKey("User")]
        [Display(Name = "Utilizador")]
        public string UtilizadorFK { get; set; }
        //Relaciona o objeto DISLIKES com um USER
        public virtual ApplicationUser User { get; set; }

        //Relaciona o objeto DISLIKES com um COMENTARIO
        public virtual Comentarios Comentarios { get; set; }
        //cria o atributo que irá funcionar como FK na BD
        //e relaciona-o com o atributo a seguir
        [ForeignKey("Comentarios")]
        [Display(Name = "Comentário")]
        public int? ComentarioFK { get; set; }
    }
}