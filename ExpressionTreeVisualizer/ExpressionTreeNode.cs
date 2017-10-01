using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace ExpressionVisualizer
{
    public class ExpressionTreeNode : INode
    {

        public ExpressionTreeNode(Object value)
        {
            Type type = value.GetType();
            Text = type.ObtainOriginalName();

            if (value is Expression)
            {
                ImageIndex = 2;
                SelectedImageIndex = 2;

                PropertyInfo[] propertyInfos = null;
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
                    if ((propertyInfo.Name != "nodeType"))
                    {
                        Nodes.Add(new AttributeNode(value, propertyInfo));
                    }
                }
            }
            else
            {
                ImageIndex = 4;
                SelectedImageIndex = 4;
                Text = "\"" + value.ToString() + "\"";
            }
        }

        public int SelectedImageIndex { get; set; }

        public int ImageIndex { get; set; }

        public string Text { get; set; }

        public List<INode> Nodes { get; } = new List<INode>();
    }
}