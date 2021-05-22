using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MapPoints.MapPoints
{
    /// <summary>
    /// Converts the Dominion Directory format to Voxel Map points
    /// </summary>
    public class ConvertDirectoryToVoxelPoints : IConvertDirectoryToVoxelPoints
    {
        /// <summary>
        /// The suffix with Y level and icon
        /// </summary>
        private const string markerSuffix = ",y:64,enabled:false,red:0.0,green:0.9254902,blue:0.8509804,suffix:diamond,world:Overworld,dimensions:overworld#";

        /// <summary>
        /// Converts the sheet format to Voxel Map points
        /// </summary>
        /// <param name="pointFileInSheetFormat">A file containing a copy paste from the directory</param>
        /// <param name="prefixToInclude">The prefix to include on the line. This ensures you can recognise the lines again.</param>
        /// <returns>A list of <see cref="string"/>s which are in the Voxel Map format</returns>
        public List<string> ConvertDirectorySheetToVoxelPoints(string pointFileInSheetFormat, string prefixToInclude)
        {
            var convertedFile = new List<string>();

            if(File.Exists(pointFileInSheetFormat))
            {
                string[] pointFileContents = File.ReadAllLines(pointFileInSheetFormat);
                foreach (string line in pointFileContents)
                {
                    // The sheet is split by tabs
                    // Example:
                    // Ad Agency	110	120	We Distribute your Ads	Maplebear333
                    string[] tabSplitLine = line.Split('	');
                    if(tabSplitLine.Length >= 5)
                    {
                        // This is the format in Voxel
                        // Example: 
                        // name:[S]Ad Agency - Maplebear333,x:110,z:120,y:64,enabled:true,red:0.0,green:0.9254902,blue:0.8509804,suffix:diamond,world:Overworld,dimensions:overworld#
                        string newLine = $"name:{prefixToInclude}{tabSplitLine[0]} - {tabSplitLine[4]}," +
                            $"x:{tabSplitLine[1]},z:{tabSplitLine[2]}{markerSuffix}";

                        convertedFile.Add(newLine);
                    }
                }
            }

            return convertedFile;
        }
    }
}
