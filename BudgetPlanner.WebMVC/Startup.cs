using Autofac;
using Autofac.Integration.Mvc;
using BudgetPlanner.Services;
using Microsoft.Owin;
using Owin;
using System.Web.Mvc;

[assembly: OwinStartupAttribute(typeof(BudgetPlanner.WebMVC.Startup))]
namespace BudgetPlanner.WebMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ADD DEPENDENCY INJECTION INHERITANCE HERE
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            //OPTIONAL:Register web abstractions like HttpContextBase.
            builder.RegisterModule<AutofacWebTypesModule>();

            builder.RegisterType<BudgetService>().As<IBudgetService>();

            //Set the dependency resolver to be Autofac.
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            ConfigureAuth(app);
        }
    }
}
