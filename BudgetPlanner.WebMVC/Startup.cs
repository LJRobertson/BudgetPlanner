using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BudgetPlanner.WebMVC.Startup))]
namespace BudgetPlanner.WebMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ADD DEPENDENCY INJECTION INHERITANCE HERE
            ConfigureAuth(app);
        }
    }
}
