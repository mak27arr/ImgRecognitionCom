using System;
using System.Collections.Generic;
using System.Text;

namespace TextRecognitionComObject.Stract
{
    class Coordinates
    {
        private int x, y;
        private int maxx, maxy;
        private bool set_max = false;
        public int X { get { return x; } 
            set {
                if (value > 0)
                {
                    if (set_max)
                    {
                        if (value <= maxx) 
                        { x = value; } 
                        else 
                        { x = maxx; }
                    }
                    else
                    {
                        x = value;
                    }
                }
            } }
        public int Y { get { return y; } 
            set {
                if (value > 0)
                {
                    if (set_max)
                    {
                        if (value <= maxy)
                        { y = value; }
                        else
                        { y = maxy; }
                    }
                    else
                    {
                        y = value;
                    }
                }
            } }
        public Coordinates(int x,int y)
        {
            this.X = x;
            this.Y = y;    
        }

        public void SetMaxValue(int x,int y)
        {
            this.maxx = x;
            this.maxy = y;
            set_max = true;
        }
    }
}
