using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _55rCheckerWF
{
    public static class ImageHelper
    {
        public static Icon MakeIcon(double percent)
        {
            Bitmap image = new Bitmap(48, 48);
            Graphics g = Graphics.FromImage(image);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
            g.Clear(Color.White);
            Rectangle rectangle = new Rectangle(0, (int)Math.Ceiling(48 * percent), 48, 48 - (int)Math.Ceiling(48 * percent));
            g.FillRectangle(Brushes.Green, rectangle);
            Icon icon = Icon.FromHandle(image.GetHicon());
            g.Dispose();
            return icon;
        }
    }
}
