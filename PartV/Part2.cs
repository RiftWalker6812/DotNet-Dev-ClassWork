using System;
using System.IO;
using System.Reflection;

namespace PartV
{
    partial class Program
    {
        private static void LateBindingT()
        {
            Console.WriteLine("***** Fun with Late Binding *****");
            // Try to load a local copy of CarLibrary.
            Assembly a;
            try
            {
                a = Assembly.Load("Squid-Math");
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
            if (a != null)
                CreateUsingLateBinding(a);
            Console.ReadLine();
            return;

            void CreateUsingLateBinding(Assembly asm)
            {
                try
                {
                    // Get metadata for the Minivan type.
                    Type SObj = asm.GetType("Squid_Math.Square");
                    // Create a Minivan instance on the fly.
                    object obj = Activator.CreateInstance(SObj);
                    Console.WriteLine("Created a {0} using late binding!", obj);

                    MethodInfo mi = SObj.GetMethod("Draw");
                    mi.Invoke(obj, null);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}