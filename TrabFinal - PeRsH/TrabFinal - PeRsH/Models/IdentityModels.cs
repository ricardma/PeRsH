using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.ComponentModel;

namespace TrabFinal___PeRsH.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Nickname")]
        [Required]
        public string Nickname { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime dataNasc { get; set; }

        public string Avatar { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        //classe que vai gerar a base de dados 

        //**********************************************************************************
        //******************************** CONSTRUTORES ************************************
        //**********************************************************************************
        public ApplicationDbContext()
        : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // não podemos usar a chave seguinte, nesta geração de tabelas
            // por causa das tabelas do Identity (gestão de utilizadores)
            // modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //
            //modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            //modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);
        }

        //***********************************************************************************
        //*********************** INSTRUÇÕES PARA CRIAR TABELAS *****************************
        //***********************************************************************************
        public virtual DbSet<Temas> Temas { get; set; }
        public virtual DbSet<Discussoes> Discussoes { get; set; }
        public virtual DbSet<Comentarios> Comentarios { get; set; }
        public virtual DbSet<ComentReports> ComentReports { get; set; }
        public virtual DbSet<Reports> Reports { get; set; }
        public virtual DbSet<Likes> Likes { get; set; }
        public virtual DbSet<Dislikes> Dislikes { get; set; }
        public virtual DbSet<Avaliacao> Avaliacao { get; set; }
    }


}