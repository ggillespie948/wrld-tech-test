using System;
using System.Collections.Generic;
using System.Text;

namespace WrldTechTest.KDimensionTree
{
    public class KDTree
    {
        public KDNode Root { get; private set; }
        public List<KDNode> Nodes { get; private set; }
        public int Count { get; set; }

        public KDTree()
        {
            Root = null;
            Count = 0;
            Nodes = new List<KDNode>();
        }

        public void BuildTree(KDNode currentNode, List<Feature> features)
        {

        }

    }
}
