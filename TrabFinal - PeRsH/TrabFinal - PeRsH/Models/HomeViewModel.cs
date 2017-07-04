using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrabFinal___PeRsH.Models
{
    public class HomeViewModel
    {
        public IEnumerable<Temas> temas;
        public IEnumerable<Discussoes> discussoes;
        public IEnumerable<Comentarios> comentarios;
        public IEnumerable<Likes> likes;
        public IEnumerable<Dislikes> dislikes;
        public IEnumerable<Avaliacao> avaliacao;
    }
}