using System.Collections.Generic;

namespace expressiontrees.ExpressionTreeVisualizer
{
    public interface INode
    {
        List<INode> Nodes { get; }
        string Text { get; }
    }
}