using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Squid_Math
{
    public static class SCalc
    {
        
    }

    public struct Rectangle
    {
        public int Width, Height;
        public Rectangle(int w, int h) : this()
        {
            Width = w; Height = h;
        }
        //Remove Later on!
        public void Draw()
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                    Console.WriteLine("*");
                Console.WriteLine();
            }
        }
    }

    public class Point : IComparable<Point>
    {
        public long X, Y;

        public Point(long x, long y)
        {
            X = x; Y = y;
        }

        public override string ToString()
        {
            return $"[{X}, {Y}]";
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        
        public int CompareTo(Point other) => (X > other.X & Y > other.Y) ? 1 : (X < other.X & Y < other.Y) ? -1 : 0;

        //Incarnation Operators
        public static Point operator + (Point p1, Point p2) => new Point(p1.X + p2.X, p1.Y + p2.Y);
        public static Point operator - (Point p1, Point p2) => new Point(p1.X - p2.X, p1.Y - p2.Y);
        public static Point operator * (Point p1, Point p2) => new Point(p1.X * p2.X, p1.Y * p2.Y);
        public static Point operator / (Point p1, Point p2) => new Point(p1.X / p2.X, p1.Y / p2.Y);
        public static bool operator == (Point p1, Point p2) => p1.Equals(p2);
        public static bool operator != (Point p1, Point p2) => !p1.Equals(p2);
        //Comparison Operators
        public static bool operator <(Point p1, Point p2) => p1.CompareTo(p2) < 0;
        public static bool operator >(Point p1, Point p2) => p1.CompareTo(p2) > 0;
        public static bool operator <=(Point p1, Point p2) => p1.CompareTo(p2) <= 0;
        public static bool operator >=(Point p1, Point p2) => p1.CompareTo(p2) >= 0;
    }
}
