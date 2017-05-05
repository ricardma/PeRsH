using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

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

        //***********************************************************************************
        //*********************** INSTRUÇÕES PARA CRIAR TABELAS *****************************
        //***********************************************************************************
        public virtual DbSet<Temas> Temas { get; set; }
        public virtual DbSet<Discussoes> Discussoes { get; set; }
        public virtual DbSet<Comentarios> Comentarios { get; set; }
        public virtual DbSet<ComentReports> ComentReports { get; set; }
        public virtual DbSet<Reports> Reports { get; set; }
    }


}