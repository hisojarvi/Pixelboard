using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pixelboard
{
    public class Palette
    {
        public Color[] Colors { get; }

        public Palette(Color[] colors)
        {
            Colors = colors;
        }

        public static Palette GetDefaultPalette()
        {
            var colors = new Color[] { new Color(0, 0, 0),
                                       new Color(255, 0, 0),
                                       new Color(128, 128, 128),
                                       new Color(255, 255, 255) };

            return new Palette(colors);
        }
    }
}