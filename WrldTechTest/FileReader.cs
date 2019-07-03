using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WrldTechTest
{
    public class FileReader
    {
        public List<Feature> ParseFeaturesFromFile(string filePath)
        {
            List<Feature> features = new List<Feature>();
            StreamReader reader = File.OpenText(filePath);
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] items = line.Split(' ');
                features.Add(new Feature(items[0], int.Parse(items[1]), int.Parse(items[2]) ));
            }
            return features;
        }

    }
}
