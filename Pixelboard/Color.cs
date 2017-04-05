using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pixelboard
{
    public struct Color
    {
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }

        public Color(byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
        }
    }
}