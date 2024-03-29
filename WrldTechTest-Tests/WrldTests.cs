using System;
using System.Collections.Generic;
using System.Linq;
using WrldTechTest;
using WrldTechTest.KDimensionTree;
using Xunit;

namespace WrldTechTest_Tests
{
    public class WrldTests
    {
        [Fact]
        public void Generate_Features_From_File_Works_As_Expected()
        {
            FileReader reader = new FileReader();
            var featuresSmall = reader.ParseFeaturesFromFile("problem_small.txt");
            var featuresBig = reader.ParseFeaturesFromFile("problem_big.txt");

            Assert.Equal(7, featuresSmall.Count);
            Assert.Equal(100001, featuresBig.Count);
        }

        [Fact]
        public void Build_KD_Tree_Works_As_Expected()
        {
            var features = new List<Feature>();
            features.Add(new Feature("placeA", 1, 1));
            features.Add(new Feature("placeB", 1, 2));
            features.Add(new Feature("placeC", 1, 3));

            var tree = new KDTree();
            tree.BuildTree(tree.Root, features, true);

            Assert.Equal(3, tree.Count);
        }

        [Fact]
        public void Find_Most_Isolated_Point_Works_As_Expected_Small_Sample()
        {
            FileReader reader = new FileReader();
            var features = reader.ParseFeaturesFromFile("problem_small.txt");

            var tree = new KDTree();
            tree.BuildTree(tree.Root, features, true);

            var node = tree.FindMostIsolatedNode();

            Assert.Equal("place6", node.Feature.Name);
        }

        [Fact]
        public void Find_Most_Isolated_Point_Works_As_Expected_Big_Sample()
        {
            FileReader reader = new FileReader();
            var features = reader.ParseFeaturesFromFile("problem_big.txt");

            var tree = new KDTree();
            tree.BuildTree(tree.Root, features, true);

            var node = tree.FindMostIsolatedNode();

            Assert.Equal("place55163", node.Feature.Name);
        }
    
    }
}
