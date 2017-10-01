using System;
using System.Text;
using System.Drawing;
using System.IO;
using System.Reflection;
using Microsoft.CSharp;
using System.Collections;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Linq.Expressions;
using ExpressionVisualizer;

namespace ExpressionVisualizer
{

//    public class ExpressionTreeVisualizerObjectSource : VisualizerObjectSource
//    {
//        public override void GetData(object target, Stream outgoingData)
//        {
//            Expression expr = (Expression)target;
//            ExpressionTreeNode browser = new ExpressionTreeNode(expr);
//            ExpressionTreeContainer container = new ExpressionTreeContainer(browser, expr.ToString());
//
//            VisualizerObjectSource.Serialize(outgoingData, container);
//        }
//    }
//
//    public class ExpressionTreeContainer
//    {
//        public ExpressionTreeContainer(ExpressionTreeNode tree, string expression)
//        {
//            this.tree = tree;
//            this.expression = expression;
//
//        }
//        private ExpressionTreeNode tree;
//
//        public ExpressionTreeNode Tree
//        {
//            get { return tree; }
//            set { tree = value; }
//        }
//        private string expression;
//
//        public string Expression
//        {
//            get { return expression; }
//            set { expression = value; }
//        }
//    }
}