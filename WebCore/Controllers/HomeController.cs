using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebCore.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public FileResult CountDown()
        {
            int width = 40, height = 40;
            int count_i = 10, count_j = 10;
            Bitmap bmp = new Bitmap(Convert.ToInt32((count_i * width) + (count_i * count_i - count_i)), Convert.ToInt32((count_j * height) + (count_j * count_j - count_j)), PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(bmp);
            int input = (DateTime.ParseExact("2017/07/06 08:00", "yyyy/MM/dd HH:mm", CultureInfo.InvariantCulture) - DateTime.Now).Days;
            int h = 0;
            for (int i = 0; i < count_i; i++)
            {
                for (int j = 0; j < count_j; j++)
                {
                    h++;
                    SolidBrush b;
                    if (h <= input)
                    {
                        b = new SolidBrush(Color.Green);
                    }
                    else
                    {
                        b = new SolidBrush(Color.LightGray);
                    }
                    g.FillRectangle(b, new Rectangle(i * (width + count_i), j * (height + count_j), width, height));
                }
            }
            using (var stream = new MemoryStream())
            {
                bmp.Save(stream, ImageFormat.Png);
                return File(stream.ToArray(), "image/bmp");
            }
        }
    }
}