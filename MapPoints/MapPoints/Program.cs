using MapPoints.MapPoints;
using System;
using System.Collections.Generic;

namespace MapPoints
{
    class Program
    {
        static void Main(string[] args)
        {
            IConvertDirectoryToVoxelPoints convertDirectoryToVoxelPoints = new ConvertDirectoryToVoxelPoints();
            IUpdateMapPoints updateMapPoints = new UpdateMapPoints(convertDirectoryToVoxelPoints);

            string outputFile = "";
            string sheetExport = "";
            string prefix = "";
            bool doOver = false;

            while (true)
            {
                if (!doOver)
                {
                    Console.WriteLine("Enter output file:");
                    outputFile = Console.ReadLine();

                    Console.WriteLine("Enter sheet export file:");
                    sheetExport = Console.ReadLine();

                    Console.WriteLine("Enter marker prexfix:");
                    prefix = Console.ReadLine();
                }


                if (!updateMapPoints.UpdateFromFile(prefix, outputFile, sheetExport, out List<string> errors))
                {
                    Console.WriteLine("Something went wrong:");
                    foreach (var error in errors)
                    {
                        Console.WriteLine(error);
                    }
                }
                else
                {
                    Console.WriteLine("Completed");
                }

                while (true)
                {
                    Console.WriteLine("Write \"again\" to try again, or anything else to enter new items");

                    string option = Console.ReadLine();

                    doOver = option.ToLower() == "again";
                }
            }
        }
    }
}
