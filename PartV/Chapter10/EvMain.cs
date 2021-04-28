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
            public static void EMain()
            {
                //Page 435
            }
        }
    }
}
