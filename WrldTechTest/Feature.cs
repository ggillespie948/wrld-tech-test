using System;
using System.Collections.Generic;
using System.Text;

namespace WrldTechTest
{
    public class Feature
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string Name { get; set; }

        public Feature(string name, int x, int y)
        {
            Name = name;
            X = x;
            Y = y;
        }
    }
}
