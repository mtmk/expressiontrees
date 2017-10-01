using System;
using System.Linq.Expressions;
using expressiontrees.ExpressionTreeVisualizer;

namespace expressiontrees
{
    class Program
    {
        static void Main(string[] args)
        {
            var p1 = Expression.Parameter(typeof(int), "num");
            var lam1 = Expression.Lambda<Func<int, bool>>(
                Expression.LessThan(
                    p1, Expression.Constant(5)
                ),
                p1
            );
            
            Console.WriteLine(lam1.Compile()(3));
            
            //Expression<Func<int, bool>> lambda = num => num < 5;  
            Expression<Func<int?, bool>> lambda = num => (num ?? 3) < 5;  
            
            ExperimentalDump(lambda);

            var node = new ExpressionTreeNode(lambda);
            Dump(node);
        }

        static void Dump(INode node, int indent = 0)
        {
            Console.WriteLine($"{new string(' ', indent)}{node.Text}");
            foreach (var nodeNode in node.Nodes)
            {
                Dump(nodeNode, indent + 2);
            }
        }
        
        private static void ExperimentalDump(LambdaExpression lambda)
        {
            Console.WriteLine($"{lambda}");

            Console.WriteLine($"{lambda.NodeType}");
            Console.WriteLine($"  Body:{lambda.Body.NodeType}");
            foreach (var p in lambda.Parameters)
            {
                Console.WriteLine($"    Param: {p.Name}: {p.NodeType}");
            }

            if (lambda.Body.NodeType == ExpressionType.LessThan)
            {
                var b = lambda.Body as BinaryExpression;
                Console.WriteLine($"    [{b.NodeType} ( {b.Left.NodeType} , {b.Right.NodeType})]");

                var p1 = b.Left as ParameterExpression;
                var p2 = b.Right as ParameterExpression;
                //Console.WriteLine($"    [{b.NodeType} ( {p1.Name}:{p1.NodeType} , {p2.Name}:{p2.NodeType})]");
            }
        }

        
    }
}
