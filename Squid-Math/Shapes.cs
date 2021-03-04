using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Squid_Math
{
    public struct Rectangle
    {
        public int Width, Height;
        public Rectangle(int w, int h)
        {
            Width = w; Height = h;
        }
        //Remove Later on!
        public void Draw()
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                    Console.Write("*");
                Console.WriteLine();
            }
        }
        public override string ToString()
        {
            return string.Format("[Width = {0}; Height = {1}]", Width, Height);
        }
    }
    public struct Square
    {
        public int Length { get; set; }
        public Square(int l) => Length = l;
        public void Draw()
        {
            for (int i = 0; i < Length; i++)
            {
                for (int j = 0; j < Length; j++)
                    Console.Write("*");
                Console.WriteLine();
            }
        }
        public override string ToString()
        {
            return string.Format("[Length = {0}]", Length);
        }
        // Rectangles can be explicitly converted
        // into Squares.
        public static explicit operator Square(Rectangle r)
        {
            Square s = new Square();
            s.Length = r.Height;
            return s;
        }
    }
}
