using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Reflection;
using System.CodeDom;
using System.CodeDom.Compiler;

namespace Kaikei
{
	public static class MathEvalJMP
	{
		
		private static string funcprefix = "using System;\r\n"
+ "public delegate void Proc();\r\n"
+ "public class Wrapper { \r\n"
+ " public static object Set(string name, object value) { \r\n"
+ " AppDomain.CurrentDomain.SetData(name, value);\r\n"
+ " return value; \r\n"
+ " }\r\n"
+ " public static object Get(string name) { \r\n"
+ " return AppDomain.CurrentDomain.GetData(name);\r\n"
+ " }\r\n"
+ " public static object Invoke(Proc proc) { \r\n"
+ " proc();\r\n"
+ " return null; \r\n"
+ " }\r\n"
+ " public static object Eval() { return \r\n";
static string funcsuffix = "; \r\n} }";


public static string StringEval(string expr)
{
string program = funcprefix + expr + funcsuffix;
CompilerParameters cp = new CompilerParameters();
cp.GenerateExecutable = false;
cp.GenerateInMemory = true;

CompilerResults results =
CodeDomProvider.CreateProvider("C#").CompileAssemblyFromSource(cp, new
string[]{program});
if ( results.Errors.HasErrors )
{
if ( results.Errors[0].ErrorNumber == "CS0029" )
return StringEval("Invoke(delegate { " + expr + "; })");
throw new Exception(results.Errors[0].ErrorText);
}
else
{
Assembly assm = results.CompiledAssembly;
Type target = assm.GetType("Wrapper");
MethodInfo method = target.GetMethod("Eval");
object result = method.Invoke(null, null);
return result == null ? null : result.ToString();
}
}
	}
}