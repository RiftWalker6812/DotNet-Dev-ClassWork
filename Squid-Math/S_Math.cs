﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Squid_Math
{
    public static class SCalc
    {
        
    }

    

    public class Point : IComparable<Point>
    {
        public long X, Y;

        public Point(long x, long y) => (X, Y) = (x, y);
        public void Deconstruct(out long x, out long y) => (x, y) = (X, Y);

        public override string ToString()
        {
            return $"[{X}, {Y}]";
        }

        public override bool Equals(object obj)
        {
            return obj.GetHashCode() == GetHashCode();
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
        
        public int CompareTo(Point other) => (X > other.X & Y > other.Y) ? 1 : (X < other.X & Y < other.Y) ? -1 : 0;

        //OPERATORS
        #region 
        //Unary Operators
        public static Point operator ++(Point p1) => new Point(p1.X+1, p1.Y+1);
        public static Point operator --(Point p1) => new Point(p1.X-1, p1.Y-1);
        //Incarnation Operators
        public static Point operator + (Point p1, Point p2) => new Point(p1.X + p2.X, p1.Y + p2.Y);
        public static Point operator - (Point p1, Point p2) => new Point(p1.X - p2.X, p1.Y - p2.Y);
        public static Point operator * (Point p1, Point p2) => new Point(p1.X * p2.X, p1.Y * p2.Y);
        public static Point operator / (Point p1, Point p2) => new Point(p1.X / p2.X, p1.Y / p2.Y);
        public static bool operator == (Point p1, Point p2) => p1.Equals(p2);
        public static bool operator != (Point p1, Point p2) => !p1.Equals(p2);
        //Comparison Operators
        public static bool operator < (Point p1, Point p2) => p1.CompareTo(p2) < 0;
        public static bool operator > (Point p1, Point p2) => p1.CompareTo(p2) > 0;
        public static bool operator <= (Point p1, Point p2) => p1.CompareTo(p2) <= 0;
        public static bool operator >= (Point p1, Point p2) => p1.CompareTo(p2) >= 0;
        #endregion 
    }
}
