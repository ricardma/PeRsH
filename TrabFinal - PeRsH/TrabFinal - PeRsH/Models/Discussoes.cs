﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrabFinal___PeRsH.Models
{
    //Representará os dados da tabela das DISCUSSOES
    public class Discussoes
    {
        public Discussoes()
        {
            //Carregar a lista de COMENTARIOS
            ListaComentarios = new HashSet<Comentarios>();
        }

        [Key]
        [Required]
        public int idDiscussao { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Data da Publicação")]
        public DateTime dataPublicacao { get; set; }

        [Required]
        [Display(Name = "Título")]
        public string titulo { get; set; }

        [Required]
        [Display(Name = "Discussão")]
        public string conteudo { get; set; }

        [Display(Name = "Gosto")]
        public int likes { get; set; }

        [Display(Name = "Não Gosto")]
        public int dislikes { get; set; }

        [Display(Name = "Número de Reports")]
        public int report { get; set; }

        //especificar que uma DISCUSSAO tem vários COMENTARIOS
        public ICollection<Comentarios> ListaComentarios { get; set; }

        //********************************
        //*CRIACAO DAS CHAVES FORASTEIRAS*
        //********************************
        //cria o atributo que irá funcionar como FK na BD
        //e relaciona-o com o atributo a seguir
        [ForeignKey("Tema")]
        [Display(Name = "Temas")]
        public int TemaFK { get; set; }
        //Relaciona o objeto DISCUSSOES com um TEMA
        public virtual Temas Tema { get; set; }

        //cria o atributo que irá funcionar como FK na BD
        //e relaciona-o com o atributo a seguir
        [ForeignKey("User")]
        [Display(Name = "User")]
        public string UtilizadorFK { get; set; }
        //Relaciona o objeto DISCUSSOES com um USER
        public virtual ApplicationUser User { get; set; }
    }
}