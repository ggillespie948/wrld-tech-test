using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WrldTechTest;
using Xunit;

namespace WrldTechTest_Tests
{
    public class FeatureHelperTests
    {
        [Fact]
        public void SplitFeatureList_Works_As_Expected()
        {
            var features = new List<Feature>();
            features.Add(new Feature("place1", 1, 2));
            features.Add(new Feature("place2", 2, 2));
            features.Add(new Feature("place3", 3, 2));
            features.Add(new Feature("place4", 4, 2));
            features.Add(new Feature("place5", 5, 2));
            features.Add(new Feature("place6", 6, 2));
            features.Add(new Feature("place7", 7, 2));

            var expectedList1 = new List<Feature>();
            expectedList1.Add(new Feature("place1", 1, 2));
            expectedList1.Add(new Feature("place2", 2, 2));
            expectedList1.Add(new Feature("place3", 3, 2));

            var expectedList2 = new List<Feature>();
            expectedList2.Add(new Feature("place5", 5, 2));
            expectedList2.Add(new Feature("place6", 6, 2));
            expectedList2.Add(new Feature("place7", 7, 2));

            var outcomeLists = FeatureHelper.SplitFeatures(features, 3);

            for(int i= 0; i < outcomeLists[0].Count; i++)
            {
                Assert.Equal(outcomeLists[0][i].Name, expectedList1[i].Name);
            }

            for (int i = 0; i < outcomeLists[1].Count; i++)
            {
                Assert.Equal(outcomeLists[1][i].Name, expectedList2[i].Name);
            }

        }

        [Fact]
        public void GetDistanceBetweenFeatures_Works_As_Expected()
        {
            var feat1 = new Feature("place1", 0, 0);
            var feat2 = new Feature("place2", 0, 5);

            var distance = FeatureHelper.GetDistance(feat2, feat1);

            Assert.Equal(5, distance);
        }

        [Fact]
        public void GetMedianFeature_Works_As_Expected()
        {
            var features = new List<Feature>();
            features.Add(new Feature("place1", 1, 2));
            features.Add(new Feature("place2", 2, 2));
            features.Add(new Feature("place3", 3, 2));
            features.Add(new Feature("place4", 4, 2));
            features.Add(new Feature("place5", 5, 2));
            features.Add(new Feature("place6", 6, 2));
            features.Add(new Feature("place7", 7, 2));

            //sort by X in test but Y is also used during building process
            features = features.OrderBy(i => i.X).ToList();

            var medianFeature = FeatureHelper.GetMedianFeatureFromList(features);

            Assert.Equal("place4", medianFeature.Name);
        }
    }
}
