using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Pixelboard
{
    public class Canvas
    {
        public int Width { get; }
        public int Height { get; }
        public Palette Palette { get; }
        public Color[] Contents { get; }

        public Canvas(int width, int height, Palette palette)
        {
            Width = width;
            Height = height;
            Palette = palette;
            Contents = new Color[width * height];
        }

        public void SetPixel(int x, int y, int paletteIndex)
        {
            if (x < 0 || y < 0 || x >= Width || y >= Height 
                || paletteIndex < 0 || paletteIndex >= Palette.Colors.Length)
            {
                return;
            }
            else
            {
                var i = y * Width + x;
                Contents[i] = Palette.Colors[paletteIndex];
            }
        }

        public Color GetPixel(int x, int y)
        {
            if (x < 0 || y < 0 || x >= Width || y >= Height)
            {
                return new Pixelboard.Color(0,0,0);
            }
            else
            {
                var i = y * Width + x;
                return Contents[i];
            }         
        }

        public void RandomizeColors()
        {
            var r = new Random();
            for(var i = 0; i < Contents.Length; i++)
            {
                var colorIndex = r.Next(Palette.Colors.Length);
                Contents[i] = Palette.Colors[colorIndex];                
            }

        }
    }
}