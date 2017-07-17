using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public class Size
    {
        public int width { get; set; }
        public int height { get; set; }

        public Size(int w, int h)
        {
            width = w;
            height = h;
        }
    }
}
