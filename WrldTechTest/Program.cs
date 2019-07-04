using System;
using System.Collections.Generic;
using WrldTechTest.KDimensionTree;

namespace WrldTechTest
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Use '-f big' to find the most isolated point in problem_big.txt");
                Console.WriteLine("Use '-f small' to find the most isolated point in problem_small.txt");
                Console.WriteLine("Use '-n' to init a new kd tree and enter features manually.");
                args = Console.ReadLine().Split(" ");
            }

            switch(args[0])
            {
                case "-f":

                    if(args.Length >= 1)
                    {
                        switch(args[1])
                        {
                            case "big":
                                FindMostIsolatedBigSet();
                                break;

                            case "small":
                                FindMostIsolatedSmallSet();
                                break;

                            default:
                                break;
                        }

                    }
                    break;

                case "-n":
                    BuildTree();
                    break;

                default:
                    Console.WriteLine("Please enter a valid command");
                    break;
            }
        }

        public static void FindMostIsolatedBigSet()
        {
            FileReader reader = new FileReader();
            var features = reader.ParseFeaturesFromFile("problem_big.txt");

            var tree = new KDTree();
            tree.BuildTree(tree.Root, features, true);

            var node = tree.FindMostIsolatedNode();

            Console.WriteLine("Most isolated feature: " + node.Feature.Name);
            Console.ReadLine();
        }

        public static void FindMostIsolatedSmallSet()
        {
            FileReader reader = new FileReader();
            var features = reader.ParseFeaturesFromFile("problem_small.txt");

            var tree = new KDTree();
            tree.BuildTree(tree.Root, features, true);

            var node = tree.FindMostIsolatedNode();

            Console.WriteLine("Most isolated feature: " + node.Feature.Name);
            Console.ReadLine();
        }

        public static void BuildTree()
        {
            Console.WriteLine("Use format: 'featureName X Y' to add feature to the tree.");
            Console.WriteLine("e.g. 'place4 100 300'");
            Console.WriteLine("enter 'exit' to stop entering features");

            var tree = new KDTree();

            var features = new List<Feature>();

            while (true)
            {
                var input = Console.ReadLine();
                if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }

                var featureString = input.Split(" ");
                if(featureString == null || featureString.Length != 3 || int.Parse(featureString[1]) == -1 || int.Parse(featureString[2]) == -1)
                {
                    Console.WriteLine("Please follow the given input format");
                    return;
                }

                features.Add(new Feature(featureString[0], int.Parse(featureString[1]), int.Parse(featureString[2])));
                Console.WriteLine();
                Console.WriteLine("Node added.");
                Console.WriteLine("Use format: 'featureName X Y' to add feature to the tree.");
                Console.WriteLine("enter 'exit' to stop entering features");

            }

            tree.BuildTree(tree.Root, features, true);
            var node = tree.FindMostIsolatedNode();
            Console.WriteLine("Most isolated feature: " + node.Feature.Name);
            Console.ReadLine();
        }

    }
}
