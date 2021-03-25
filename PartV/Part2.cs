using System;
using System.IO;
using System.Reflection;

namespace PartV
{
    partial class Program
    {
        //Methods

            //LateBindings
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

                    MethodInfo mi = SObj.GetMethod("Draw2");
                    mi.Invoke(obj, new object[] { 5 });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

            //Attributes
        private static void AttrTests()
        {
            HorseAndBuggy mule = new HorseAndBuggy();
        }


        //Models
        
            //Attr Part 1
        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false)]
        public sealed class VehicleDescriptionAttribute : Attribute
        {
            public string Description;
            public VehicleDescriptionAttribute(string vehicalDescription) =>
                Description = vehicalDescription;
            public VehicleDescriptionAttribute() { }
        }
        // Assign description using a "named property."
        [Serializable]
        [VehicleDescription(Description = "My rocking Harley")]
        public class Motorcycle
        {
        }
        [Serializable]
        [Obsolete("Use another vehicle!")]
        [VehicleDescription("The old gray mare, she ain't what she used to be...")]
        public class HorseAndBuggy
        {
        }
        [VehicleDescription("A very long, slow, but feature-rich auto")]
        public class Winnebago
        {
            [VehicleDescription("My rocking CD player")]
            public void PlayMusic(bool On)
            {
                
            }
        }
        // This enumeration defines the possible targets of an attribute.
        public enum AttributeTargets
        {
            All, Assembly, Class, Constructor,
            Delegate, Enum, Event, Field, GenericParameter,
            Interface, Method, Module, Parameter,
            Property, ReturnValue, Struct
        }
    }
}