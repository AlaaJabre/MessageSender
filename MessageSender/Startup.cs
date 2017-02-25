using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MessageSender.Startup))]
namespace MessageSender
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {            
        }
    }
}
