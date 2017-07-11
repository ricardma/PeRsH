namespace TrabFinal___PeRsH.Migrations
{
    using Microsoft.AspNet.Identity;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security;
    using System.Web;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Web.Security;

    internal sealed class Configuration : DbMigrationsConfiguration<TrabFinal___PeRsH.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        
        protected override void Seed(TrabFinal___PeRsH.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            
            var tema = new List<Temas>
            {
                new Temas { idTema = 1, Etiqueta = "História" },
                new Temas { idTema = 2, Etiqueta = "Regras" },
                new Temas { idTema = 3, Etiqueta = "Competições Nacionais" },
                new Temas { idTema = 4, Etiqueta = "Competições Internacionais" },
                new Temas { idTema = 5, Etiqueta = "Escalões" },
                new Temas { idTema = 6, Etiqueta = "Patins" },
                new Temas { idTema = 7, Etiqueta = "Caneleiras" },
                new Temas { idTema = 8, Etiqueta = "Joelheiras" },
                new Temas { idTema = 9 , Etiqueta = "Luvas" }
            };
            tema.ForEach(tt => context.Temas.AddOrUpdate(t => t.idTema, tt));
            context.SaveChanges();

            var discussoes = new List<Discussoes>
            {
                new Discussoes { idDiscussao = 1, titulo= "Lorem ipsum dolor sit amet, consectetuer adipiscing elit.", conteudo = "Praesent mauris ante, elementum et, bibendum at, posuere sit amet, nibh. Duis tincidunt lectus quis dui viverra vestibulum. Suspendisse vulputate aliquam dui. Nulla elementum dui ut augue. Aliquam vehicula mi at mauris. Maecenas placerat, nisl at consequat rhoncus, sem nunc gravida justo, quis eleifend arcu velit quis lacus.Morbi magna magna, tincidunt a, mattis non, imperdiet vitae, tellus. Sed odio est, auctor ac, sollicitudin in, consequat vitae, orci. Fusce id felis. Vivamus sollicitudin metus eget eros.", dataPublicacao = new DateTime(2016,6,7), dislikes = 0, likes = 0, UtilizadorFK = context.Users.OrderBy(x=>x.Id).Skip(0).FirstOrDefault().Id, TemasDiscussoes = new List<Temas> { tema[2],tema[3]} }, 
                new Discussoes { idDiscussao = 2, titulo= "Lorem ipsum dolor sit amet, consectetuer adipiscing elit.2", conteudo = "Praesent mauris ante, elementum et, bibendum at, posuere sit amet, nibh. Duis tincidunt lectus quis dui viverra vestibulum. Suspendisse vulputate aliquam dui. Nulla elementum dui ut augue. Aliquam vehicula mi at mauris. Maecenas placerat, nisl at consequat rhoncus, sem nunc gravida justo, quis eleifend arcu velit quis lacus.Morbi magna magna, tincidunt a, mattis non, imperdiet vitae, tellus. Sed odio est, auctor ac, sollicitudin in, consequat vitae, orci. Fusce id felis. Vivamus sollicitudin metus eget eros.2", dataPublicacao = new DateTime(2016,6,10), dislikes = 0, likes = 0, UtilizadorFK = context.Users.OrderBy(x=>x.Id).Skip(0).FirstOrDefault().Id, TemasDiscussoes = new List<Temas> { tema[2],tema[3] } },
                new Discussoes { idDiscussao = 3, titulo= "Lorem ipsum dolor sit amet, consectetuer adipiscing elit3", conteudo = "Praesent mauris ante, elementum et, bibendum at, posuere sit amet, nibh. Duis tincidunt lectus quis dui viverra vestibulum. Suspendisse vulputate aliquam dui. Nulla elementum dui ut augue. Aliquam vehicula mi at mauris. Maecenas placerat, nisl at consequat rhoncus, sem nunc gravida justo, quis eleifend arcu velit quis lacus.Morbi magna magna, tincidunt a, mattis non, imperdiet vitae, tellus. Sed odio est, auctor ac, sollicitudin in, consequat vitae, orci. Fusce id felis. Vivamus sollicitudin metus eget eros3", dataPublicacao = new DateTime(2016,7,9), dislikes = 0, likes = 0,  UtilizadorFK = context.Users.OrderBy(x=>x.Id).Skip(0).FirstOrDefault().Id, TemasDiscussoes = new List<Temas> { tema[1],tema[2],tema[3] } },
                new Discussoes { idDiscussao = 4, titulo= "Lorem ipsum dolor sit amet, consectetuer adipiscing elit.4", conteudo = "Praesent mauris ante, elementum et, bibendum at, posuere sit amet, nibh. Duis tincidunt lectus quis dui viverra vestibulum. Suspendisse vulputate aliquam dui. Nulla elementum dui ut augue. Aliquam vehicula mi at mauris. Maecenas placerat, nisl at consequat rhoncus, sem nunc gravida justo, quis eleifend arcu velit quis lacus.Morbi magna magna, tincidunt a, mattis non, imperdiet vitae, tellus. Sed odio est, auctor ac, sollicitudin in, consequat vitae, orci. Fusce id felis. Vivamus sollicitudin metus eget eros.4", dataPublicacao = new DateTime(2016,8,10), dislikes = 0, likes = 0,  UtilizadorFK = context.Users.OrderBy(x=>x.Id).Skip(0).FirstOrDefault().Id, TemasDiscussoes = new List<Temas> { tema[5] } },
                new Discussoes { idDiscussao = 5, titulo= "Lorem ipsum dolor sit amet, consectetuer adipiscing elit.5", conteudo = "Praesent mauris ante, elementum et, bibendum at, posuere sit amet, nibh. Duis tincidunt lectus quis dui viverra vestibulum. Suspendisse vulputate aliquam dui. Nulla elementum dui ut augue. Aliquam vehicula mi at mauris. Maecenas placerat, nisl at consequat rhoncus, sem nunc gravida justo, quis eleifend arcu velit quis lacus.Morbi magna magna, tincidunt a, mattis non, imperdiet vitae, tellus. Sed odio est, auctor ac, sollicitudin in, consequat vitae, orci. Fusce id felis. Vivamus sollicitudin metus eget eros.5", dataPublicacao = new DateTime(2016,9,7), dislikes = 0, likes = 0,  UtilizadorFK = context.Users.OrderBy(x=>x.Id).Skip(0).FirstOrDefault().Id, TemasDiscussoes = new List<Temas> { tema[6] } },
                new Discussoes { idDiscussao = 6, titulo= "Lorem ipsum dolor sit amet, consectetuer adipiscing elit.6", conteudo = "Praesent mauris ante, elementum et, bibendum at, posuere sit amet, nibh. Duis tincidunt lectus quis dui viverra vestibulum. Suspendisse vulputate aliquam dui. Nulla elementum dui ut augue. Aliquam vehicula mi at mauris. Maecenas placerat, nisl at consequat rhoncus, sem nunc gravida justo, quis eleifend arcu velit quis lacus.Morbi magna magna, tincidunt a, mattis non, imperdiet vitae, tellus. Sed odio est, auctor ac, sollicitudin in, consequat vitae, orci. Fusce id felis. Vivamus sollicitudin metus eget eros.6", dataPublicacao = new DateTime(2016,10,7), dislikes = 0, likes = 0,  UtilizadorFK = context.Users.OrderBy(x=>x.Id).Skip(0).FirstOrDefault().Id, TemasDiscussoes = new List<Temas> { tema[7] } },
            };
            discussoes.ForEach(dd => context.Discussoes.AddOrUpdate(d => d.idDiscussao, dd));
            context.SaveChanges();

            var comentarios = new List<Comentarios>
            {
                new Comentarios { comentID = 1, conteudo = "Duis tincidunt lectus quis dui viverra vestibulum. Suspendisse vulputate aliquam dui. Nulla elementum dui ut augue. Aliquam vehicula mi at mauris. Maecenas placerat, nisl at consequat rhoncus, sem nunc gravida justo, quis eleifend arcu velit quis lacus.", dataComentario = new DateTime(2016,7,8), likes = 0, dislikes = 0, DiscussaoFK = 1, UtilizadorFK = context.Users.OrderBy(x=>x.Id).Skip(1).FirstOrDefault().Id},
                new Comentarios { comentID = 2, conteudo = "Praesent mauris ante, elementum et, bibendum at, posuere sit amet, nibh.", dataComentario = new DateTime(2016,7,9), likes = 0, dislikes = 0,  DiscussaoFK = 1, UtilizadorFK = context.Users.OrderBy(x=>x.Id).Skip(0).FirstOrDefault().Id },
                new Comentarios { comentID = 3, conteudo = "Maecenas placerat, nisl at consequat rhoncus, sem nunc gravida justo, quis eleifend arcu velit quis lacus.", likes = 0, dislikes = 0, dataComentario = new DateTime(2016,7,10), DiscussaoFK = 2, UtilizadorFK = context.Users.OrderBy(x=>x.Id).Skip(1).FirstOrDefault().Id},
                new Comentarios {comentID = 4, conteudo = "Sed odio est, auctor ac, sollicitudin in, consequat vitae, orci. Fusce id felis. ", dataComentario = new DateTime(2016,7,11), likes = 0, dislikes = 0,  DiscussaoFK = 2, UtilizadorFK = context.Users.OrderBy(x=>x.Id).Skip(1).FirstOrDefault().Id},
                new Comentarios { comentID = 5, conteudo = "Praesent mauris ante, elementum et, bibendum at, posuere sit amet, nibh.", dataComentario = new DateTime(2016,7,12), likes = 0, dislikes = 0, DiscussaoFK = 2,UtilizadorFK = context.Users.OrderBy(x=>x.Id).Skip(0).FirstOrDefault().Id },
                new Comentarios { comentID = 6, conteudo = "Duis tincidunt lectus quis dui viverra vestibulum. Suspendisse vulputate aliquam dui. Nulla elementum dui ut augue. Aliquam vehicula mi at mauris. Maecenas placerat, nisl at consequat rhoncus, sem nunc gravida justo, quis eleifend arcu velit quis lacus.", dataComentario = new DateTime(2016,7,13), likes = 0, dislikes = 0,  DiscussaoFK = 3,UtilizadorFK = context.Users.OrderBy(x=>x.Id).Skip(1).FirstOrDefault().Id},
            };
            comentarios.ForEach(cc => context.Comentarios.AddOrUpdate(c => c.comentID, cc));
            context.SaveChanges();
       }
    }
}
