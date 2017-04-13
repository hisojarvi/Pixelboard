using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.AspNet.SignalR;

[assembly: OwinStartup(typeof(Pixelboard.Startup))]

namespace Pixelboard
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // TODO Read from persistent storage
            var canvas = new Canvas(15, 15, Palette.GetDefaultPalette());

            var settings = new PixelboardSettings()
            {
                Cooldown = 3
            };
            
            GlobalHost.DependencyResolver.Register(
                typeof(PixelboardHub),
                () => new PixelboardHub(canvas, settings));

            app.MapSignalR();            
        }
    }
}
