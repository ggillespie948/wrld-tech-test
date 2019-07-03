using System;
using System.Collections.Generic;
using System.Linq;
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

        public void BuildTree(KDNode currentNode, List<Feature> features, bool useXAxis)
        {
            if (useXAxis)
                features = features.OrderBy(i => i.X).ToList();
            else
                features = features.OrderBy(i => i.Y).ToList();

            Feature med = FeatureHelper.GetMedianFeatureFromList(features);
            var node = InsertNode(currentNode, med, true);

            // We now split the sorted points into 2 groups
            // 1st group [0]: points before the median
            // 2nd group [1]: Points after the median
            var splitFeatures = FeatureHelper.SplitFeatures(features, features.IndexOf(med));
            List<Feature> leftSplit = splitFeatures[0].ToList();
            List<Feature> rightSplit = splitFeatures[1].ToList();

            // We new recurse, passing the left and right arrays for arguments.
            // The current node's left and right values become the "roots" for
            // each recursion call. We also forward cycle to the next dimension.

            // We only need to recurse if the point array contains more than one point
            // If the array has no points then the node stay a null value
            if (leftSplit.Count <= 1)
            {
                if (leftSplit.Count == 1)
                {
                    InsertNode(node, leftSplit[0], useXAxis);
                }
            }
            else
            {
                this.BuildTree(node, leftSplit, !useXAxis); //alternate dimension each level we build
            }

            // Do the same for the right points
            if (rightSplit.Count <= 1)
            {
                if (rightSplit.Count == 1)
                {
                    InsertNode(node, rightSplit[0], useXAxis);
                }
            }
            else
            {
                BuildTree(node, rightSplit, !useXAxis);
            }

        }

        private KDNode InsertNode(KDNode root, Feature insertFeature, bool useXAxis)
        {
            if (root == null)
            {
                root = new KDNode(insertFeature, useXAxis);

                if (this.Root == null)
                    this.Root = root;

                Nodes.Add(root);
                Count++;

            }
            else if (root.UseXAxis) 
            {
                if (insertFeature.X < root.Feature.X)
                {
                    root.LeftNode = InsertNode(root.LeftNode, insertFeature, !useXAxis);
                }
                else
                {
                    root.RightNode = InsertNode(root.RightNode, insertFeature, !useXAxis);
                }

            }
            else 
            {
                if (insertFeature.Y < root.Feature.Y)
                {
                    root.LeftNode = InsertNode(root.LeftNode, insertFeature, !useXAxis);
                }
                else
                {
                    root.RightNode = InsertNode(root.RightNode, insertFeature, !useXAxis);
                }
            }

            return root;
        }

    }
}
