using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace PointClassifier
{
    class Ellipse
    {
        public Rectangle coords;
        public Color color;

        public Ellipse(Color colour, Rectangle rect)
        {
            this.color = colour;
            this.coords = rect;
        }
    }
}
