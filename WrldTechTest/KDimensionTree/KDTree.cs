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

            var splitFeatures = FeatureHelper.SplitFeatures(features, features.IndexOf(med));
            List<Feature> leftSplit = splitFeatures[0].ToList();
            List<Feature> rightSplit = splitFeatures[1].ToList();

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


        public double FindDistanceToNearestNeighbour(KDNode currentNode, Feature goalFeature, KDNode closestNeighbour, double closestNeighbourDistance)
        {
            if (currentNode == null)
            {
                return closestNeighbourDistance;
            }

            var currentDistance = FeatureHelper.GetDistance(currentNode.Feature, goalFeature);

            if (currentDistance < closestNeighbourDistance)
            {
                if (currentNode.Feature != goalFeature)
                {
                    closestNeighbour = currentNode;
                    closestNeighbourDistance = currentDistance;
                }
            }

            if (currentNode.UseXAxis) 
            {
                if (goalFeature.X < currentNode.Feature.X) //is goal.x LEFT current node.x? IF so, traverse LEFT, otherwise go RIGHT (easy to understand because this is X comparison node)
                {
                    closestNeighbourDistance = FindDistanceToNearestNeighbour(currentNode.LeftNode, goalFeature, closestNeighbour, closestNeighbourDistance);

                    if(Math.Abs(currentNode.Feature.X - goalFeature.X) < closestNeighbourDistance)
                        closestNeighbourDistance = FindDistanceToNearestNeighbour(currentNode.RightNode, goalFeature, closestNeighbour, closestNeighbourDistance);
                }
                else
                {
                    closestNeighbourDistance = FindDistanceToNearestNeighbour(currentNode.RightNode, goalFeature, closestNeighbour, closestNeighbourDistance);


                    if (Math.Abs(goalFeature.X - currentNode.Feature.X) < closestNeighbourDistance)
                        closestNeighbourDistance = FindDistanceToNearestNeighbour(currentNode.LeftNode, goalFeature, closestNeighbour, closestNeighbourDistance);
                }

            }
            else 
            {
                if (goalFeature.Y > currentNode.Feature.Y) //is goal.y ABOVE current node.y? IF so, traverse to RIGHT to go ABOVE (because this is a Y comparison node) otherwise go left to look down 
                {
                    closestNeighbourDistance = FindDistanceToNearestNeighbour(currentNode.RightNode, goalFeature, closestNeighbour, closestNeighbourDistance);

                    if (Math.Abs(currentNode.Feature.Y - goalFeature.Y) < closestNeighbourDistance)
                        closestNeighbourDistance = FindDistanceToNearestNeighbour(currentNode.LeftNode, goalFeature, closestNeighbour, closestNeighbourDistance);
                }
                else
                {
                    closestNeighbourDistance = FindDistanceToNearestNeighbour(currentNode.LeftNode, goalFeature, closestNeighbour, closestNeighbourDistance);

                    if (Math.Abs(goalFeature.Y - currentNode.Feature.Y) < closestNeighbourDistance)
                        closestNeighbourDistance = FindDistanceToNearestNeighbour(currentNode.RightNode, goalFeature, closestNeighbour, closestNeighbourDistance);
                }
            }

            return closestNeighbourDistance;
        }

        public KDNode FindMostIsolatedNode()
        {
            KDNode mostIsolatedNode = null;
            var mostIsolatedNodeShortestNDistance = 0.00;

            for (int i = 0; i < Nodes.Count; i++)
            {
                var shortestNDistanceToNN = FindDistanceToNearestNeighbour(Root, Nodes[i].Feature, null, double.MaxValue);

                if (shortestNDistanceToNN > mostIsolatedNodeShortestNDistance)
                {
                    mostIsolatedNode = Nodes[i];
                    mostIsolatedNodeShortestNDistance = shortestNDistanceToNN;
                }
            }

            return mostIsolatedNode;
        }

    }
}
