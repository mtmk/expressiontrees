using System;
using System.Globalization;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Collections.ObjectModel;

[assembly: CLSCompliant(true)]

namespace ExpressionVisualizer
{
    public class AttributeNode : INode
    {
        public AttributeNode(Object attribute, PropertyInfo propertyInfo)
        {
            Text = propertyInfo.Name + " : " + propertyInfo.PropertyType.ObtainOriginalName();
            ImageIndex = 3;
            SelectedImageIndex = 3;

            Object value = propertyInfo.GetValue(attribute, null);
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
                    Text += " : \"" + value.ToString() + "\"";
                }
            }
            else
            {
                Text += " : null";
            }
        }

        public int SelectedImageIndex { get; set; }

        public int ImageIndex { get; set; }

        public string Text { get; set; }

        public List<INode> Nodes { get; } = new List<INode>();
    }
}