using System;
using System.Runtime.InteropServices;

namespace ProgramD
{
	public class Program
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("Hello CIL code!");
			Console.ReadLine();
			TestOne one = new TestOne();
			one.setVariables();
			Console.WriteLine($"{one.Alpha}, {one.Beta}");
			string s = Console.ReadLine();
			MessageBox((IntPtr)0, s, "The C Message Box", 0); 
		}
		
		[DllImport("User32.dll", CharSet=CharSet.Unicode)]
		public static extern int MessageBox(IntPtr h, string m, string c, int type);
	}
	public class TestOne
	{
		public int Alpha;
		public string Beta;
		public void setVariables()
		{
			Alpha = 1;
			Beta = "The World is Open";
		}
	}
}