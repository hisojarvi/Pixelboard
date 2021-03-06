﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pixelboard
{
    public class Palette
    {
        public Color[] Colors { get; set; }

        public Palette()
        {
            Colors = new Color[] { new Color(0, 0, 0) };
        }

        public Palette(Color[] colors)
        {
            Colors = colors;
        }

        public static Palette GetDefaultPalette()
        {
            var colors = new Color[] { new Color(0, 0, 0),
                                       new Color(51, 51, 51),
                                       new Color(102, 68, 0),
                                       new Color(136, 0, 0),                                                                              
                                       new Color(0, 0, 170),
                                       new Color(119, 119, 119),                                       
                                       new Color( 0, 136, 255),
                                       new Color(0, 204, 85),
                                       new Color(255, 119, 119),
                                       new Color(221, 136, 85),
                                       new Color(204, 68, 204),
                                       new Color(170, 255, 102),
                                       new Color(187, 187, 187),
                                       new Color(170, 255, 238),
                                       new Color(238, 238, 119),
                                       new Color(255, 255, 255),
            };

            return new Palette(colors);
        }
    }
}