using System;
using WrldTechTest;
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
    }
}
