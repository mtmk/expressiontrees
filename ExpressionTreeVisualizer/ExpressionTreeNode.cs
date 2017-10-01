using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace expressiontrees.ExpressionTreeVisualizer
{
    public class ExpressionTreeNode : INode
    {

        public ExpressionTreeNode(object value)
        {
            var type = value.GetType();
            Text = type.ObtainOriginalName();

            if (value is Expression)
            {
                PropertyInfo[] propertyInfos;
                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Expression<>))
                {
                    propertyInfos = type.BaseType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                }
                else
                {
                    propertyInfos = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                }

                foreach (PropertyInfo propertyInfo in propertyInfos)
                {
                    if (propertyInfo.Name != "nodeType")
                    {
                        Nodes.Add(new AttributeNode(value, propertyInfo));
                    }
                }
            }
            else
            {
                Text = "\"" + value + "\"";
            }
        }

        public string Text { get; }

        public List<INode> Nodes { get; } = new List<INode>();
    }
}