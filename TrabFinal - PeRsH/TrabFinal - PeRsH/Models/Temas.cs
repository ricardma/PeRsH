﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrabFinal___PeRsH.Models
{
    //Representará os dados da tabela dos TEMAS
    public class Temas
    {
        //Criar o construtor desta classe
        public Temas()
        {
           //Carregar a lista de Discussões
           TemasDiscussoes = new HashSet<Discussoes>();
        }
        
        [Key]
        public int idTema { get; set; }

        [Required]
        [Display(Name = "Tema")]
        public string Etiqueta { get; set; }

        //especificar que um TEMA tem várias DISCUSSOES
        public ICollection<Discussoes> TemasDiscussoes { get; set; }
    }
}