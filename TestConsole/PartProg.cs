using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole
{
    partial class Program
    {
        struct Point2
        {
            public int x;
            public int y;
            public override string ToString()
            {
                return string.Format("({0}, {1})", x, y);
            }
        }
        unsafe static void UsePointerToPoint()
        {
            Console.WriteLine("\nField access via pointers");
            // Access members via pointer.
            Point2 point;
            Point2* p = &point;
            p->x = 100;
            p->y = 200;
            Console.WriteLine(p->ToString());
            // Access members via pointer indirection.
            Point2 point2;
            Point2* p2 = &point2;
            (*p2).x = 100;
            (*p2).y = 200;
            Console.WriteLine((*p2).ToString());
        }
        // This entire structure is "unsafe" and can
        // be used only in an unsafe context.
        unsafe struct Node
        {
            public int Value;
            public Node* Left;
            public Node* Right;
        }
        // This struct is safe, but the Node2* members
        // are not. Technically, you may access "Value" from
        // outside an unsafe context, but not "Left" and "Right".
        public struct Node2
        {
            public int Value;
            // These can be accessed only in an unsafe context!
            public unsafe Node2* Left;
            public unsafe Node2* Right;
        }
        unsafe static void SquareIntPointer(int* myIntPointer)
        {
            // Square the value just for a test.
            *myIntPointer *= *myIntPointer;
        }
        unsafe public static void UnsafeSwap(int* i, int* j)
        {
            int temp = *i;
            *i = *j;
            *j = temp;
        }
        public static void SafeSwap(ref int i, ref int j)
        {
            int temp = i;
            i = j;
            j = temp;
        }
        unsafe static void UnsafeStackAlloc()
        {
            char* p = stackalloc char[256];
            for (int k = 0; k < 256; k++)
                p[k] = (char)k;
        }
        unsafe public static void UseAndPinPoint()
        {
            PointRef pt = new PointRef
            {
                x = 5,
                y = 6
            };
            // Pin pt in place so it will not
            // be moved or GC-ed.
            fixed (int* p = &pt.x)
            {
                // Use int* variable here!
            }
            // pt is now unpinned, and ready to be GC-ed once
            // the method completes.
            Console.WriteLine("Point is: {0}", pt);
        }
        unsafe static void UseSizeOfOperator()
        {
            Console.WriteLine("The size of short is {0}.", sizeof(short));
            Console.WriteLine("The size of int is {0}.", sizeof(int));
            Console.WriteLine("The size of long is {0}.", sizeof(long));
            Console.WriteLine("The size of Point2 is {0}.", sizeof(Point2));
        }
        class PointRef // <= Renamed and retyped.
        {
            public int x;
            public int y;
            public override string ToString()
            {
                return string.Format("({0}, {1})", x, y);
            }
        }
    }
}
