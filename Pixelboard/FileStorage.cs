using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Pixelboard
{
    public class FileStorage : IPixelboardStorage
    {
        Canvas IPixelboardStorage.LoadCanvas()
        {
            string path = HttpContext.Current.Server.MapPath("~/App_Data/canvas.json");
            Stream file = new FileStream(path, FileMode.Open);
            using (var reader = new StreamReader(file))
            {
                Canvas canvas = JsonConvert.DeserializeObject<Canvas>(reader.ReadToEnd());
                return canvas;
            }
        }

        PixelboardSettings IPixelboardStorage.LoadSettings()
        {
            throw new NotImplementedException();
        }

        void IPixelboardStorage.SaveCanvas(Canvas canvas)
        {
            string path = HttpContext.Current.Server.MapPath("~/App_Data/canvas.json");
            Stream file = new FileStream(path, FileMode.Create);
            using (var writer = new StreamWriter(file))
            {
                writer.Write(JsonConvert.SerializeObject(canvas));
            }
        }

        void IPixelboardStorage.SaveSettings(PixelboardSettings settings)
        {
            throw new NotImplementedException();
        }
    }
}
