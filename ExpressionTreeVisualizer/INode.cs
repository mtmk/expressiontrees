using System.Collections.Generic;

namespace ExpressionVisualizer
{
    public interface INode
    {
        List<INode> Nodes { get; }
        string Text { get; }
    }
}