using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TrabFinal___PeRsH.Models
{
    //Representará o relacionamento entre a tabela TEMAS e DISCUSSOES
    public class TemasDiscussoes : DbContext
    {
        [Key]
        public int idTemasDiscussoes { get; set; }

        //cria o atributo que irá funcionar como FK na BD
        //e relaciona-o com o atributo a seguir
        [ForeignKey("Tema")]
        [Display(Name = "Temas")]
        public int TemaFK { get; set; }
        public virtual Temas Tema { get; set; }


        //cria o atributo que irá funcionar como FK na BD
        //e relaciona-o com o atributo a seguir
        [ForeignKey("Discussoes")]
        [Display(Name = "Discussoes")]
        public int DiscussaoFK { get; set; }
        public virtual Discussoes Discussoes { get; set; }
    }
}