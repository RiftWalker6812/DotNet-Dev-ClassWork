
//  Microsoft (R) .NET Framework IL Disassembler.  Version 4.7.3081.0
//  Copyright (c) Microsoft Corporation.  All rights reserved.



// Metadata version: v4.0.30319
.module extern User32.dll
.assembly extern mscorlib
{
  .publickeytoken = (B7 7A 5C 56 19 34 E0 89 )                         // .z\V.4..
  .ver 4:0:0:0
}
.assembly DynamicProgram
{
  .custom instance void [mscorlib]System.Runtime.CompilerServices.CompilationRelaxationsAttribute::.ctor(int32) = ( 01 00 08 00 00 00 00 00 ) 
  .custom instance void [mscorlib]System.Runtime.CompilerServices.RuntimeCompatibilityAttribute::.ctor() = ( 01 00 01 00 54 02 16 57 72 61 70 4E 6F 6E 45 78   // ....T..WrapNonEx
                                                                                                             63 65 70 74 69 6F 6E 54 68 72 6F 77 73 01 )       // ceptionThrows.

  // --- The following custom attribute is added automatically, do not uncomment -------
  //  .custom instance void [mscorlib]System.Diagnostics.DebuggableAttribute::.ctor(valuetype [mscorlib]System.Diagnostics.DebuggableAttribute/DebuggingModes) = ( 01 00 07 01 00 00 00 00 ) 

  .hash algorithm 0x00008004
  .ver 0:0:0:0
}
.module DynamicProgram.exe
// MVID: {0EDFEB05-7053-4EFD-A389-EEE0D3D9BF4A}
.imagebase 0x00400000
.file alignment 0x00000200
.stackreserve 0x00100000
.subsystem 0x0003       // WINDOWS_CUI
.corflags 0x00000001    //  ILONLY
// Image base: 0x0C280000


// =============== CLASS MEMBERS DECLARATION ===================

.class public auto ansi beforefieldinit ProgramD.Program
       extends [mscorlib]System.Object
{
  .method public hidebysig static void  Main(string[] args) cil managed
  {
    .entrypoint
    // Code size       90 (0x5a)
    .maxstack  4
    .locals init (class ProgramD.TestOne V_0,
             string V_1)
    IL_0000:  nop
    IL_0001:  ldstr      "Hello CIL code!"
    IL_0006:  call       void [mscorlib]System.Console::WriteLine(string)
    IL_000b:  nop
    IL_000c:  call       string [mscorlib]System.Console::ReadLine()
    IL_0011:  pop
    
	nop
	ldstr "HELLO"
	call void [mscorlib]System.Console::WriteLine(string)
	nop
	call       string [mscorlib]System.Console::ReadLine()
    pop
	nop
	call void CountToTen()
	ret
  } // end of method Program::Main
  
  .method public hidebysig static void CountToTen() cil managed
{
 .maxstack 2
 .locals init ([0] int32 i) // Init the local integer "i".
 IL_0000: ldc.i4.0 // Load this value onto the stack.
 IL_0001: stloc.0 // Store this value at index "0".
 IL_0002: br.s IL_0008 // Jump to IL_0008.
 IL_0004: ldloc.0 // Load value of variable at index 0.
 IL_0005: ldc.i4.1 // Load the value "1" on the stack.
 IL_0006: add // Add current value on the stack at index 0.
 IL_0007: stloc.0
 IL_0008: ldloc.0 // Load value at index "0".
 IL_0009: ldc.i4.s 10 // Load value of "10" onto the stack.
 IL_000b: blt.s IL_0004 // Less than? If so, jump back to IL_0004
 IL_000d: ret
}

  .method public hidebysig static pinvokeimpl("User32.dll" unicode winapi) 
          int32  MessageBox(native int h,
                            string m,
                            string c,
                            int32 'type') cil managed preservesig
  {
  }
  .method public hidebysig specialname rtspecialname 
          instance void  .ctor() cil managed
  {
    // Code size       8 (0x8)
    .maxstack  8
    IL_0000:  ldarg.0
    IL_0001:  call       instance void [mscorlib]System.Object::.ctor()
    IL_0006:  nop
    IL_0007:  ret
  } // end of method Program::.ctor

} // end of class ProgramD.Program

.class public auto ansi beforefieldinit ProgramD.TestOne
       extends [mscorlib]System.Object
{
  .field public int32 Alpha
  .field public string Beta
  .method public hidebysig instance void 
          setVariables() cil managed
  {
    // Code size       20 (0x14)
    .maxstack  8
    IL_0000:  nop
    IL_0001:  ldarg.0
    IL_0002:  ldc.i4.1
    IL_0003:  stfld      int32 ProgramD.TestOne::Alpha
    IL_0008:  ldarg.0
    IL_0009:  ldstr      "The World is Open"
    IL_000e:  stfld      string ProgramD.TestOne::Beta
    IL_0013:  ret
  } // end of method TestOne::setVariables

  .method public hidebysig specialname rtspecialname 
          instance void  .ctor() cil managed
  {
    // Code size       8 (0x8)
    .maxstack  8
    IL_0000:  ldarg.0
    IL_0001:  call       instance void [mscorlib]System.Object::.ctor()
    IL_0006:  nop
    IL_0007:  ret
  } // end of method TestOne::.ctor

} // end of class ProgramD.TestOne


// =============================================================

// *********** DISASSEMBLY COMPLETE ***********************
// WARNING: Created Win32 resource file C:\Users\studentam\Documents\Joshua Hernandez Work\2020-2021\STC Online\Book4\Docs\Extras\DynamicProgram\DynamicProgram.res
