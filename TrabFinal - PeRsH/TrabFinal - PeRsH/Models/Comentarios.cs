using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrabFinal___PeRsH.Models
{
    //Representará os dados da tabela COMENTARIOS
    public class Comentarios
    {
        [Key]
        public int comentID { get; set; }

        [Required]
        [Display(Name = "Comentário")]
        public string conteudo { get; set; }

        [Column(TypeName = "date")]
        public DateTime dataComentario { get; set; }

        [Display(Name = "Gosto")]
        public int likes { get; set; }

        [Display(Name = "Não Gosto")]
        public int dislikes { get; set; }

        [Display(Name = "Número de Reports")]
        public int report { get; set; }

        //********************************
        //*CRIACAO DAS CHAVES FORASTEIRAS*
        //********************************
        //cria o atributo que irá funcionar como FK na BD
        //e relaciona-o com o atributo a seguir
        [ForeignKey("Discussoes")]
        [Display(Name = "Discussoes")]
        public int DiscussaoFK { get; set; }
        //Relaciona o objeto COMENTARIO com uma DISCUSSAO
        public virtual Discussoes Discussoes { get; set; }

        //cria o atributo que irá funcionar como FK na BD
        //e relaciona-o com o atributo a seguir
        [ForeignKey("User")]
        [Display(Name = "User")]
        public string UtilizadorFK { get; set; }
        //Relaciona o objeto COMENTARIO com um USER
        public virtual ApplicationUser User { get; set; }
    }
}