using System;
using System.Reflection;
using System.Text;

namespace expressiontrees.ExpressionTreeVisualizer
{
    static class ExpressionTreeExtention 
    {
        static string ExtractName(string name)
        {
            int i = name.LastIndexOf("`", StringComparison.Ordinal);
            if (i > 0)
                name = name.Substring(0, i);
            return name;
        }

        static string ExtractGenericArguments(Type[] names)
        {
            StringBuilder builder = new StringBuilder("<");
            foreach (Type genericArgument in names) {
                if (builder.Length != 1) builder.Append(", ");
                builder.Append(ObtainOriginalName(genericArgument));
            }
            builder.Append(">");
            return builder.ToString();
        }

        public static string ObtainOriginalName(this Type type)
        {
            if (!type.IsGenericType) {
                return type.Name;
            }
            else {
                return ExtractName(type.Name) + ExtractGenericArguments(type.GetGenericArguments());
            }
        }

        public static string ObtainOriginalMethodName(this MethodInfo method)
        {
            if (!method.IsGenericMethod) {
                return method.Name;
            }
            else {
                return ExtractName(method.Name) + ExtractGenericArguments(method.GetGenericArguments());
            }
        }
    }
}