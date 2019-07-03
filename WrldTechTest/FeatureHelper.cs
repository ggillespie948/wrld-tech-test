using System;
using System.Collections.Generic;
using System.Text;

namespace WrldTechTest
{
    public static class FeatureHelper
    {
        /// <summary>
        /// Method which returns the median feature in a given list of features. Used during the
        /// building process of Kd Trees.
        /// </summary>
        /// <param name="features"> List of all features </param>
        /// <returns> The median feature in the list </returns>
        public static Feature GetMedianFeatureFromList(List<Feature> features)
        {
            int middle = ((features.Count) / 2);
            if (middle % 2 == 0)
            {
                int middleIndex = Convert.ToInt32(middle - .5);
                return features[middleIndex];
            }
            else
            {
                return features[middle];
            }
        }

        public static double GetDistance(Feature feature1, Feature feature2)
        {
            return Math.Sqrt(Math.Pow((feature2.X - feature1.X), 2) + Math.Pow((feature2.Y - feature1.Y), 2));
        }

        /// <summary>
        /// Break a list of feature into 2 chunks either side of a given index
        /// </summary>
        public static List<List<Feature>> SplitFeatures(List<Feature> source, int chunkElementIndex)
        {
            List<List<Feature>> chunks = new List<List<Feature>>();
            chunks.Add(new List<Feature>());
            chunks.Add(new List<Feature>());

            for (int i = 0; i < source.Count; i++)
            {
                if (i < chunkElementIndex)
                    chunks[0].Add(source[i]);
                else if (i > chunkElementIndex)
                    chunks[1].Add(source[i]);
            }

            return chunks;
        }
    }
}
