using System;

using System.IO;
using System.Reflection;

using System.Collections.Generic;
using System.Linq;
using System.Text;

[assembly: CLSCompliant(true)]

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
        private static void AttrTests()
        {
            HorseAndBuggy mule = new HorseAndBuggy();
        }
        private static void RefleAttr()
        {
            Console.WriteLine("***** Value of VehicleDescriptionAttribute *****\n");
            ReflectOnAttributesUsingEarlyBinding();
            //ReflectAttributesUsingLateBinding();
            Console.ReadLine();
            return;

            void ReflectOnAttributesUsingEarlyBinding()
            {
                // Get a Type representing the Winnebago.
                Type t = typeof(Winnebago);
                // Get all attributes on the Winnebago.
                object[] customAtts = t.GetCustomAttributes(false);
                // Print the description.
                foreach (VehicleDescriptionAttribute v in customAtts)
                    Console.WriteLine("-> {0}\n", v.Description);
            }
            void ReflectAttributesUsingLateBinding()
            {
                try
                {
                    // Load the local copy of AttributedCarLibrary.
                    Assembly asm = Assembly.Load("AttributedCarLibrary");
                    // Get type info of VehicleDescriptionAttribute.
                    Type vehicleDesc =
                    asm.GetType("AttributedCarLibrary.VehicleDescriptionAttribute");
                    // Get type info of the Description property.
                    PropertyInfo propDesc = vehicleDesc.GetProperty("Description");
                    // Get all types in the assembly.
                    Type[] types = asm.GetTypes();
                    // Iterate over each type and obtain any VehicleDescriptionAttributes.
                    foreach (Type t in types)
                    {
                        object[] objs = t.GetCustomAttributes(vehicleDesc, false);
                        // Iterate over each VehicleDescriptionAttribute and print
                        // the description using late binding.
                        foreach (object o in objs)
                        {
                            Console.WriteLine("-> {0}: {1}\n",
                            t.Name, propDesc.GetValue(o, null));
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        //Models
        #region Part2

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
            //[VehicleDescription("My rocking CD player")]
            public void PlayMusic(bool On)
            {
                
            }
        }
        // This enumeration defines the possible targets of an attribute.
        //public enum AttributeTargets
        //{
        //    All, Assembly, Class, Constructor,
        //    Delegate, Enum, Event, Field, GenericParameter,
        //    Interface, Method, Module, Parameter,
        //    Property, ReturnValue, Struct
        //}
        #endregion
    }
}
