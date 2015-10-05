﻿using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace PowerAssert.Infrastructure.Nodes
{
    class NewObjectNode : Node
    {
        [NotNull]
        public List<Node> Parameters { get; set; }

        [NotNull]
        public string Type { get; set; }

        [NotNull]
        public string Value { get; set; }

        internal override void Walk(NodeWalker walker, int depth, bool wrap)
        {
            walker("new " + Type + "(", Value, depth);
            foreach (var node in Parameters.Take(1))
            {
                node.Walk(walker, depth, false);
            }
            foreach (var node in Parameters.Skip(1))
            {
                walker(", ");
                node.Walk(walker, depth, false);
            }
            walker(")");
        }
    }

    class MemberInitNode : Node
    {
        [NotNull]
        public NewObjectNode Constructor { get; set; }

        [NotNull]
        public List<Node> Bindings { get; set; }

        internal override void Walk(NodeWalker walker, int depth, bool wrap)
        {
            Constructor.Walk(walker, depth, false);
            walker("{");
            foreach (var node in Bindings.Take(1))
            {
                node.Walk(walker, depth + 1, false);
            }
            foreach (var node in Bindings.Skip(1))
            {
                walker(", ");
                node.Walk(walker, depth + 1, false);
            }
            walker("}");
        }
    }

    class ListInitNode : Node
    {
        [NotNull]
        public NewObjectNode Constructor { get; set; }

        [NotNull]
        public List<Node> Items { get; set; }

        internal override void Walk(NodeWalker walker, int depth, bool wrap)
        {
            Constructor.Walk(walker, depth, false);
            walker("{");
            foreach (var node in Items.Take(1))
            {
                node.Walk(walker, depth + 1, false);
            }
            foreach (var node in Items.Skip(1))
            {
                walker(", ");
                node.Walk(walker, depth + 1, false);
            }
            walker("}");
        }
    }

    class MemberAssignmentNode : Node
    {
        [NotNull]
        public string MemberName { get; set; }

        [NotNull]
        public Node Value { get; set; }

        internal override void Walk(NodeWalker walker, int depth, bool wrap)
        {
            walker(MemberName);
            walker(" = ");
            Value.Walk(walker, depth, false);
        }
    }
}