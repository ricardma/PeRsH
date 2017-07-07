using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using System.Configuration;
using TrabFinal___PeRsH.Models;

[assembly: OwinStartupAttribute(typeof(TrabFinal___PeRsH.Startup))]
namespace TrabFinal___PeRsH
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            //método utilizado para inicializar os dados na BD
            //Dados - Roles e Utilizadores
            iniciaApp();
        }

        /// <summary>
        /// cria primeiro as Roles de Administração e o de Utilizador, caso não existam
        /// cria também utilizador, caso crie as Roles
        /// </summary>
        private void iniciaApp()
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            //criação da Role Admin
            if (!roleManager.RoleExists("Admin"))
            {
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                //criação do utilizador desta role
                var user = new ApplicationUser();
                user.UserName = "admin@admin.com";
                user.Email = "admin@admin.com";
                user.Nickname = "admin";
                user.Avatar = "DefaultAvatar.jpg";
                string passwd = "123Qwe.";
                var chkUser = userManager.Create(user,passwd);

                if (chkUser.Succeeded)
                {
                    var result = userManager.AddToRole(user.Id,role.Name);
                }
            }

            //criação da Role Utilizador
            if (!roleManager.RoleExists("Utilizador"))
            {
                var role = new IdentityRole();
                role.Name = "Utilizador";
                roleManager.Create(role);


                //criação do utilizador desta role
                var user = new ApplicationUser();
                user.UserName = "utilizador@utilizador.com";
                user.Email = "utilizador@utilizador.com";
                user.Nickname = "utilizador";
                user.Avatar = "DefaultAvatar.jpg";
                string passwd = "123Qwe.";
                var chkUser = userManager.Create(user, passwd);

                if (chkUser.Succeeded)
                {
                    var result = userManager.AddToRole(user.Id, role.Name);
                }
            }
        }
    }
}


