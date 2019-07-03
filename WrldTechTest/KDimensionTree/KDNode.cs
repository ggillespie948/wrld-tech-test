using System;
using System.Collections.Generic;
using System.Text;

namespace WrldTechTest.KDimensionTree
{
    public class KDNode
    {
        public KDNode LeftNode { get; set; }
        public KDNode RightNode { get; set; }
        public Feature Feature { get; set; }
        public bool IsLeaf { get; set; }

        public KDNode(Feature feature)
        {
            Feature = feature;
            RightNode = null;
            LeftNode = null;
        }
    }
}
