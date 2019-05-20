using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NetBlade.Mask.MVC.Teste.Startup))]
namespace NetBlade.Mask.MVC.Teste
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
