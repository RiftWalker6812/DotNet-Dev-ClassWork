using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartV.Chapter10
{
    public class EvMain
    {
        public EvMain()//Instantiate
        {
            //Delegates
            DSpace.DMain();
            //Events
            ESpace.EMain();
            //Anonymous
            ASpace.AMain();
        }
        //Delegates
        private class DSpace
        {
            
            public static void DMain()
            {
                SimpleDelageExample();
                DelegateEventEnablers();
                GenericDelegates();
                ActionsAndFuncs();
                return;
            }
            #region M1
            private static void SimpleDelageExample()
            {
                Console.WriteLine("***** Simple Delegate Example *****\n");
                SimpleMath m = new SimpleMath();
                BinaryOp b = new BinaryOp(m.Add);
                DisplayDelegateInfo(b);
                Console.WriteLine("10 + 10 is {0}", b(10, 10));
                Console.ReadLine();
            }
            private static void DisplayDelegateInfo(Delegate delObj)
            {
                // Print the names of each member in the
                // delegate's invocation list.
                foreach (Delegate d in delObj.GetInvocationList())
                {
                    Console.WriteLine("Method Name: {0}", d.Method);
                    Console.WriteLine("Type Name: {0}", d.Target);
                }
            }
            public delegate int BinaryOp(int x, int y);
            // This class contains methods BinaryOp will
            // point to.
            public class SimpleMath
            {
                public int Add(int x, int y)
                { return x + y; }
                public static int Subtract(int x, int y)
                { return x - y; }
            }
            #endregion
            #region M2
            private static void DelegateEventEnablers()
            {
                Console.WriteLine("***** Delegates as event enablers *****\n");
                Car c1 = new Car("SlugBug", 100, 10);
                c1.RegisterWithCarEngine(new Car.CarEngineHandler(OnCarEngineEvent));

                Car.CarEngineHandler handler2 = new Car.CarEngineHandler(OnCarEngineEvent2);
                c1.RegisterWithCarEngine(handler2);

                Console.WriteLine("***** Speeding up *****");
                for (int i = 0; i < 6; i++)
                    c1.Accelerate(20);

                c1.UnRegisterWithCarEngine(handler2);

                Console.WriteLine("***** Speeding up *****");
                for (int i = 0; i < 6; i++)
                    c1.Accelerate(20);

                Console.ReadLine();
            }

            private static void OnCarEngineEvent2(string msg)
            {
                Console.WriteLine("=> {0}", msg.ToUpper());
            }

            private static void OnCarEngineEvent(string msg)
            {
                Console.WriteLine("\n***** Message From Car Object *****");
                Console.WriteLine("=> {0}", msg);
                Console.WriteLine("***********************************\n");
            }


            public class Car
            {
                //Internal state data.
                public int CurrentSpeed, MaxSpeed = 100;
                public string PetName;

                private bool carIsDead;

                public Car() { }
                public Car(string name, int maxSp, int currSp) =>
                    (PetName, MaxSpeed, CurrentSpeed) = (name, maxSp, currSp);

                // 1) Define a delegate type.
                public delegate void CarEngineHandler(string msgForCaller);
                // 2) Define a member variable of this delegate.
                private CarEngineHandler listOfhandlers;
                // 3) Add registration function for the caller.
                // Now with multicasting support!
                public void RegisterWithCarEngine(CarEngineHandler methodToCall) =>
                    listOfhandlers += methodToCall;
                // 3.5) Remove method from the delegate
                public void UnRegisterWithCarEngine(CarEngineHandler methodToCall) =>
                    listOfhandlers -= methodToCall;
                // 4) Implement the Accelerate() method to invoke the delegate's
                // invocation list under the correct circumstances.
                public void Accelerate(int delta)
                {
                    if (carIsDead && (listOfhandlers != null))
                        listOfhandlers("Sorry, this car is dead...");
                    else
                    {
                        CurrentSpeed += delta;

                        //Is this car "almost dead"?
                        if (10 >= (MaxSpeed - CurrentSpeed) && listOfhandlers != null)
                            listOfhandlers("Careful buddy! Gonna blow!");
                        if (CurrentSpeed >= MaxSpeed)
                            carIsDead = true;
                        else
                            Console.WriteLine("CurrentSpeed = {0}", CurrentSpeed);
                    }
                }
            }
            #endregion
            #region M3

            private delegate void MyGenericDelegate<T>(T args);
            private static void GenericDelegates()
            {
                Console.WriteLine("***** Generic Delegates *****\n");

                MyGenericDelegate<string> strTarget = new MyGenericDelegate<string>(StringTarget);
                strTarget("Some string data");

                MyGenericDelegate<int> intTarget = new MyGenericDelegate<int>(IntTarget);
                intTarget(9);
                Console.ReadLine();
            }
            private static void StringTarget(string arg)
            {
                Console.WriteLine("arg in uppercase is: {0}", arg.ToUpper());
            }
            private static void IntTarget(int arg)
            {
                Console.WriteLine("++arg is: {0}", ++arg);
            } //page 430


            #endregion
            #region M4


            private static void ActionsAndFuncs()
            {
                Console.WriteLine("***** Fun with Action and Func *****");

                Action<string, ConsoleColor, int> actionTarget = new Action<string, ConsoleColor, int>(DisplayMessage);
                actionTarget("Action Message!", ConsoleColor.DarkBlue, 5);

                Console.ReadLine();

                Func<int, int, int> funcTarget = Add;
                int result = funcTarget.Invoke(40, 40);
                Console.WriteLine("40 + 40 = {0}", result);

                Func<int, int, string> funcTarget2 = new Func<int, int, string>(SumToString);
                string sum = funcTarget2(90, 300);
                Console.WriteLine(sum);
            }
            private static void DisplayMessage(string msg, ConsoleColor txtColor, int printCount)
            {
                ConsoleColor previous = Console.ForegroundColor;
                Console.ForegroundColor = txtColor;

                for (int i = 0; i < printCount; i++)
                    Console.WriteLine(msg);
                Console.ForegroundColor = previous;
            }
            private static int Add(int x, int y) => x + y;
            private static string SumToString(int x, int y) => (x + y).ToString();
            #endregion

            
        }
        //Events
        private class ESpace
        {
            //Properties

            //Methods
            public static void EMain()
            {
                Console.WriteLine("***** Prim and Proper Events *****\n");
                Car c1 = new Car("SlugBug", 100, 10);

                //Register event handlers
                c1.AboutToBlow += CarIsAlmostDoomed;
                c1.AboutToBlow += CarAboutToBlow;

                c1.Exploded += CarExploded;

                Console.WriteLine("***** Speeding up *****");
                for (int i = 0; i < 6; i++)
                    c1.Accelerate(20);

                c1.Exploded -= CarExploded;

                Console.WriteLine("\n***** Speeding up *****");
                for (int i = 0; i < 6; i++)
                    c1.Accelerate(20);

                Console.ReadLine();
            }
            private static void CarIsAlmostDoomed(object sender, CarEventArgs e) => Console.WriteLine(e.msg);
            private static void CarAboutToBlow(object sender, CarEventArgs e) => 
                Console.WriteLine("{0} says: {1}", sender, e.msg);
            private static void CarExploded(object sender, CarEventArgs e) => Console.WriteLine(e.msg);
            //Classes
            public class Car
            {
                //Init
                public Car(string s, int m, int c) => (CarName, MaxSpeed, CurrentSpeed) = (s, m, c);
                //Create Delegate Type
                //public delegate void CarEngineHandler(string msg);
                //public delegate void CarEngineHandler(object sender, CarEventArgs e);
                //Declare Events
                public event EventHandler<CarEventArgs> Exploded;
                public event EventHandler<CarEventArgs> AboutToBlow;
                //Properties
                private string CarName;
                private bool IsCarDead;
                private int CurrentSpeed;
                private int MaxSpeed;
                //Methods
                public void Accelerate(int delta)
                {
                    if (IsCarDead)
                        Exploded?.Invoke(this, new CarEventArgs("Sorry, this car is dead..."));
                    else
                    {
                        CurrentSpeed += delta;

                        if (10 == MaxSpeed - CurrentSpeed)
                            AboutToBlow?.Invoke(this, new CarEventArgs("Careful buddy! Gonna blow!"));
                        if (!(IsCarDead = CurrentSpeed >= MaxSpeed))
                            Console.WriteLine("CurrentSpeed = {0}", CurrentSpeed);
                    }
                }
            }
            public class CarEventArgs : EventArgs
            {
                public readonly string msg;
                public CarEventArgs(string message) => msg = message;
            }
        }
        //Anonymous
        private class ASpace
        {
            public static void AMain()
            {
                Console.WriteLine("***** Anonymous Methods *****\n");
                int aboutToBlowCounter = 0;

                ESpace.Car c1 = new ESpace.Car("SlugBug", 100, 10);

                c1.AboutToBlow += delegate
                {
                    aboutToBlowCounter++;
                    Console.WriteLine("Eek! Going too fast!");
                };

                c1.AboutToBlow += delegate(object sender, ESpace.CarEventArgs e) 
                { Console.WriteLine("Critical Message from Car: {0}", e.msg); };

                for (int i = 0; i < 6; i++)
                    c1.Accelerate(20);

                Console.Write("AboutToBlow event was fired {0} times.", aboutToBlowCounter);
                Console.ReadLine();

                Console.WriteLine("***** Fun with Lambdas *****\n");
                TraditionalDelegateSyntax();
                Console.ReadLine();

                Console.WriteLine("***** More Fun with Lambdas *****\n");
                // Make a car as usual.
                var c2 = new ESpace.Car("SlugBug", 100, 10);
                // Hook into events with lambdas!
                c2.AboutToBlow += (sender, e) => { Console.WriteLine(e.msg); };
                c2.Exploded += (sender, e) => { Console.WriteLine(e.msg); };
                // Speed up (this will generate the events).
                Console.WriteLine("\n***** Speeding up *****");
                for (int i = 0; i < 6; i++)
                    c2.Accelerate(20);
                Console.ReadLine();
            }
            private static void TraditionalDelegateSyntax()
            {
                // Make a list of integers.
                List<int> list = new List<int>();
                list.AddRange(new int[] { 20, 1, 4, 8, 9, 44 });
                // Call FindAll() using traditional delegate syntax.
                Predicate<int> callback = IsEvenNumber;
                List<int> evenNumbers = list.FindAll(callback);
                Console.WriteLine("Here are your even numbers:");
                foreach (int evenNumber in evenNumbers)
                {
                    Console.Write("{0}\t", evenNumber);
                }
                Console.WriteLine();
            }
            // Target for the Predicate<> delegate.
            private static bool IsEvenNumber(int i)
            {
                // Is it an even number?
                return (i % 2) == 0;
            }
            private static void LambdaExpressionSyntax()
            {
                // Make a list of integers.
                List<int> list = new List<int>();
                list.AddRange(new int[] { 20, 1, 4, 8, 9, 44 });
                // Now, use a C# lambda expression.
                List<int> evenNumbers = list.FindAll(i => (i % 2) == 0);
                Console.WriteLine("Here are your even numbers:");
                foreach (int evenNumber in evenNumbers)
                {
                    Console.Write("{0}\t", evenNumber);
                }
                Console.WriteLine();
            }
        }
    }
}
