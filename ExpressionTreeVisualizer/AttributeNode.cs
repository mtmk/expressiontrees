using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;

[assembly: CLSCompliant(true)]

namespace expressiontrees.ExpressionTreeVisualizer
{
    public class AttributeNode : INode
    {
        public AttributeNode(object attribute, PropertyInfo propertyInfo)
        {
            Text = propertyInfo.Name + " : " + propertyInfo.PropertyType.ObtainOriginalName();

            var value = propertyInfo.GetValue(attribute, null);
            if (value != null)
            {
                if (value.GetType().IsGenericType && value.GetType().GetGenericTypeDefinition() == typeof(ReadOnlyCollection<>)) {
                    if ((int)value.GetType().InvokeMember("get_Count", BindingFlags.InvokeMethod, null, value, null, CultureInfo.InvariantCulture) == 0)
                    {
                        Text += " : Empty";
                    }
                    else
                    {
                        foreach (object tree in (IEnumerable)value)
                        {
                            if (tree is Expression)
                            {
                                Nodes.Add(new ExpressionTreeNode(tree));
                            }
                            else if (tree is MemberAssignment)
                            {
                                Nodes.Add(new ExpressionTreeNode(((MemberAssignment)tree).Expression));
                            }
                        }
                    }
                }
                else if (value is Expression)
                {
                    Text += ((Expression)value).NodeType;
                    Nodes.Add(new ExpressionTreeNode(value));
                }
                else if (value is MethodInfo)
                {
                    MethodInfo minfo = value as MethodInfo;
                    Text += " : \"" + minfo.ObtainOriginalMethodName() + "\"";
                }
                else if (value is Type)
                {
                    Type minfo = value as Type;
                    Text += " : \"" + minfo.ObtainOriginalName() + "\"";
                }
                else
                {
                    Text += " : \"" + value + "\"";
                }
            }
            else
            {
                Text += " : null";
            }
        }

        public string Text { get; }

        public List<INode> Nodes { get; } = new List<INode>();
    }
}