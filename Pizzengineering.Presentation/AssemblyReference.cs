using System.Reflection;

namespace Pizzengineering.Presentation;

public static class AssemblyReference
{
	public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}