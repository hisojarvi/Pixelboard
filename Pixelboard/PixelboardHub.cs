using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace Pixelboard
{
    public class PixelboardHub : Hub
    {
        private Canvas Canvas;
        private PixelboardSettings Settings;

        public PixelboardHub(Canvas canvas, PixelboardSettings settings)
        {
            Canvas = canvas;
            Settings = settings;
        }

        public void RandomizeCanvas()
        {
            Canvas.RandomizeColors();
            Clients.All.sendCanvas(Canvas.Width, Canvas.Height, Canvas.Contents);
        }

        public void PutPixel(int x, int y, int colorIndex)
        {
            Canvas.SetPixel(x, y, colorIndex);
            Clients.Caller.sendCooldown(Settings.Cooldown);
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