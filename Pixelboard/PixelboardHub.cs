using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace Pixelboard
{
    public class PixelboardHub : Hub
    {
        private static Canvas Canvas = new Canvas(100, 100, Palette.GetDefaultPalette());
        private static int Cooldown = 10;

        public PixelboardHub()
        {
        }
        
        public void RandomizeCanvas()
        {
            Canvas.RandomizeColors();
            Clients.All.sendCanvas(Canvas.Width, Canvas.Height, Canvas.Contents);
        }

        public void PutPixel(int x, int y, int colorIndex)
        {
            Canvas.SetPixel(x, y, colorIndex);
            Clients.Caller.sendCooldown(Cooldown);
            Clients.All.broadcastPixel(x, y, Canvas.GetPixel(x, y));
        }

        // A user requests canvas after joining the session
        public void RequestCanvas()
        {            
            Clients.Caller.sendCanvas(Canvas.Width, Canvas.Height, Canvas.Contents);
        }

        // A user requests canvas after joining the session
        public void RequestPalette()
        {
            Clients.Caller.sendPalette(Canvas.Palette.Colors);
        }


    }
}