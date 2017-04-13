using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixelboard
{
    public interface IPixelboardStorage
    {
        void SaveCanvas(Canvas canvas);
        Canvas LoadCanvas();

        void SaveSettings(PixelboardSettings settings);
        PixelboardSettings LoadSettings();
    }
}
