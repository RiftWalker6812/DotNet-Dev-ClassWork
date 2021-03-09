using System;
using System.Collections.Generic;
using Squid_Math;

namespace TestConsole
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("c\ns\nx\na\nt\np\ne\n");
            one:   
            switch (Console.Read())
            {
                case 'c' : C_PointTests();
                    break;
                case 's': DrawingShapes();
                    break;
                case 'x': ExtensionTest();
                    break;
                case 'a': AnnoyingExtenTest();
                    break;
                case 't': AnonymousTests();
                    break;
                case 'e': Environment.Exit(0);
                    break;
                case 'p': PointerTests();
                    break;
                default:
                    goto one;
            }
            Console.WriteLine("\nEnd\nBut you can still Continue!");
            goto one;
        }

        private static void C_PointTests()
        {
            Console.WriteLine("***** Fun with Overloaded Operators *****\n");
            // Make two points.
            Point ptOne = new Point(100, 100);
            Point ptTwo = new Point(40, 40);
            Console.WriteLine("ptOne = {0}", ptOne);
            Console.WriteLine("ptTwo = {0}", ptTwo);
            // Add the points to make a bigger point?
            Console.WriteLine("ptOne + ptTwo: {0} ", ptOne + ptTwo);
            // Subtract the points to make a smaller point?
            Console.WriteLine("ptOne - ptTwo: {0} ", ptOne - ptTwo);
            Console.ReadLine();

            // Freebie +=
            Point ptThree = new Point(90, 5);
            Console.WriteLine("ptThree = {0}", ptThree);
            Console.WriteLine("ptThree += ptTwo: {0}", ptThree += ptTwo);
            // Freebie -=
            Point ptFour = new Point(0, 500);
            Console.WriteLine("ptFour = {0}", ptFour);
            Console.WriteLine("ptFour -= ptThree: {0}", ptFour -= ptThree);
            Console.ReadLine();
            // Applying the ++ and -- unary operators to a Point.
            Point ptFive = new Point(1, 1);
            Console.WriteLine("++ptFive = {0}", ++ptFive); // [2, 2]
            Console.WriteLine("--ptFive = {0}", --ptFive); // [1, 1]
            // Apply same operators as postincrement/decrement.
            Point ptSix = new Point(20, 20);
            Console.WriteLine("ptSix++ = {0}", ptSix++); // [20, 20]
            Console.WriteLine("ptSix-- = {0}", ptSix--); // [21, 21]
            Console.ReadLine();

            Console.WriteLine("ptOne == ptTwo : {0}", ptOne == ptTwo);
            Console.WriteLine("ptOne != ptTwo : {0}", ptOne != ptTwo);
            Console.ReadLine();

            Console.WriteLine("HashCode of ptOne : {0}", ptOne.GetHashCode());
            Console.ReadLine();

            Console.WriteLine("ptOne < ptTwo : {0}", ptOne < ptTwo);
            Console.WriteLine("ptOne > ptTwo : {0}", ptOne > ptTwo);
            Console.ReadLine();
        }
        private static void DrawingShapes()
        {
            Console.WriteLine("***** Fun with Conversions *****\n");
            // Make a Rectangle.
            Rectangle r = new Rectangle(15, 4);
            Console.WriteLine(r.ToString());
            r.Draw();
            Console.WriteLine();
            // Convert r into a Square,
            // based on the height of the Rectangle.
            Square s = (Square)r;
            Console.WriteLine(s.ToString());
            s.Draw();
            Console.ReadLine();
            // Convert Rectangle to Square to invoke method.
            Rectangle rect = new Rectangle(10, 5);
            DrawSquare((Square)rect);
            Console.ReadLine();
            // Converting an int to a Square.
            Square sq2 = (Square)90;
            Console.WriteLine("sq2 = {0}", sq2);
            // Converting a Square to an int.
            int side = (int)sq2;
            Console.WriteLine("Side length of sq2 = {0}", side);
            Console.ReadLine();

            Square s3 = new Square(83);
            // Attempt to make an implicit cast?
            Rectangle rect2 = s3;
            Console.WriteLine("rect2 = {0}", rect2);
            // Explicit cast syntax still OK!
            Square s4 = new Square(3);
            Rectangle rect3 = (Rectangle)s4; //redundant
            Console.WriteLine("rect3 = {0}", rect3);
            Console.ReadLine();

            return;

            void DrawSquare(Square sq)
            {
                Console.WriteLine(sq.ToString());
                sq.Draw();
            }
        }
        private static void ExtensionTest()
        {
            Console.WriteLine("***** Fun with Extension Methods *****\n");
            // The int has assumed a new identity!
            int myInt = 12345678;
            myInt.DisplayDefiningAssembly();
            // So has the DataSet!
            System.Data.DataSet d = new System.Data.DataSet();
            d.DisplayDefiningAssembly();
            // And the SoundPlayer!
            System.Media.SoundPlayer sp = new System.Media.SoundPlayer();
            sp.DisplayDefiningAssembly();
            // Use new integer functionality.
            Console.WriteLine("Value of myInt: {0}", myInt);
            Console.WriteLine("Reversed digits of myInt: {0}", myInt.ReverseDigits());
            Console.ReadLine();
        }
        private static void AnnoyingExtenTest()
        {
            Console.WriteLine("***** Extending Interface Compatible Types *****\n");
            // System.Array implements IEnumerable!
            string[] data = { "Wow", "this", "is", "sort", "of", "annoying",
                                "but", "in", "a", "weird", "way", "fun!"};
            data.PrintDataAndBeep();
            Console.WriteLine();
            // List<T> implements IEnumerable!
            List<int> myInts = new List<int>() { 10, 15, 20 };
            myInts.PrintDataAndBeep();
            Console.ReadLine();
        }
        private static void AnonymousTests()
        {
            Console.WriteLine("***** Fun with Anonymous Types *****\n");
            // Make an anonymous type representing a car.
            var myCar = new { Color = "Bright Pink", Make = "Saab", CurrentSpeed = 55 };
            // Now show the color and make.
            Console.WriteLine("My car is a {0} {1}.", myCar.Color, myCar.Make);
            // Now call our helper method to build anonymous type via args.
            BuildAnonType("BMW", "Black", 90);
            Console.ReadLine();

            Console.WriteLine("\n***** Fun with Anonymous Types *****\n");
            // Make an anonymous type representing a car.
            var myCar2 = new { Color = "Bright Pink", Make = "Saab", CurrentSpeed = 55 };
            // Reflect over what the compiler generated.
            ReflectOverAnonymousType(myCar2);
            Console.ReadLine();
            EqualityTest();
            Console.ReadLine();
            var purchaseItem = new
            {
                TimeBought = DateTime.Now,
                ItemBought = new { Color = "Red", Make = "Saab", CurrentSpeed = 55 },
                Price = 34.000
            };
            ReflectOverAnonymousType(purchaseItem);
            return;

            void BuildAnonType(string make, string color, int currSp)
            {
                // Build anon type using incoming args.
                var car = new { Make = make, Color = color, Speed = currSp };
                // Note you can now use this type to get the property data!
                Console.WriteLine("You have a {0} {1} going {2} MPH",
                car.Color, car.Make, car.Speed);
                // Anon types have custom implementations of each virtual
                // method of System.Object. For example:
                Console.WriteLine("ToString() == {0}", car.ToString());
            }
            void ReflectOverAnonymousType(object obj)
            {
                Console.WriteLine("obj is an instance of: {0}", obj.GetType().Name);
                Console.WriteLine("Base class of {0} is {1}",
                obj.GetType().Name,
                obj.GetType().BaseType);
                Console.WriteLine("obj.ToString() == {0}", obj.ToString());
                Console.WriteLine("obj.GetHashCode() == {0}", obj.GetHashCode());
                Console.WriteLine();
            }
            void EqualityTest()
            {
                // Make 2 anonymous classes with identical name/value pairs.
                var firstCar = new { Color = "Bright Pink", Make = "Saab", CurrentSpeed = 55 };
                var secondCar = new { Color = "Bright Pink", Make = "Saab", CurrentSpeed = 55 };
                // Are they considered equal when using Equals()?
                if (firstCar.Equals(secondCar))
                    Console.WriteLine("Same anonymous object!");
                else
                    Console.WriteLine("Not the same anonymous object!");
                // Are they considered equal when using ==?
                if (firstCar == secondCar)
                    Console.WriteLine("Same anonymous object!");
                else
                    Console.WriteLine("Not the same anonymous object!");
                // Are these objects the same underlying type?
                if (firstCar.GetType().Name == secondCar.GetType().Name)
                    Console.WriteLine("We are both the same type!");
                else
                    Console.WriteLine("We are different types!");
                // Show all the details.
                Console.WriteLine();
                ReflectOverAnonymousType(firstCar);
                ReflectOverAnonymousType(secondCar);
            }
        }

        #region Unsafe Area...
        unsafe static void PointerTests()
        {
            unsafe
            {
                int myInt = 10;
                SquareIntPointer(&myInt);
                Console.WriteLine("myint: {0}", myInt);
            }

            int myInt2 = 5;
            // Compiler error! Must be in unsafe context!
            SquareIntPointer(&myInt2);
            Console.WriteLine("myInt: {0}", myInt2);

            PrintValueAndAddress();

            Console.WriteLine("***** Calling method with unsafe code *****");
            // Values for swap.
            int i = 10, j = 20;
            // Swap values "safely."
            Console.WriteLine("\n***** Safe swap *****");
            Console.WriteLine("Values before safe swap: i = {0}, j = {1}", i, j);
            SafeSwap(ref i, ref j);
            Console.WriteLine("Values after safe swap: i = {0}, j = {1}", i, j);
            // Swap values "unsafely."
            Console.WriteLine("\n***** Unsafe swap *****");
            Console.WriteLine("Values before unsafe swap: i = {0}, j = {1}", i, j);
            unsafe { UnsafeSwap(&i, &j); }
            Console.WriteLine("Values after unsafe swap: i = {0}, j = {1}", i, j);
            Console.ReadLine();

            UsePointerToPoint();

            UnsafeStackAlloc();
            UseAndPinPoint();
            UseSizeOfOperator();
            
            return;

            unsafe void PrintValueAndAddress()
            {
                int myInt;
                // Define an int pointer, and
                // assign it the address of myInt.
                int* ptrToMyInt = &myInt;
                // Assign value of myInt using pointer indirection.
                *ptrToMyInt = 123;
                // Print some stats.
                Console.WriteLine("\nValue of myInt {0}", myInt);
                Console.WriteLine("Address of myInt {0:X}", (int)&ptrToMyInt);
            }
        }
        #endregion
    }
}
