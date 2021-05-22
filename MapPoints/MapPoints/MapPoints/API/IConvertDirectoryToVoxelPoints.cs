using System;
using System.Collections.Generic;
using System.Text;

namespace MapPoints.MapPoints
{
    /// <summary>
    /// Converts the Dominion Directory format to Voxel Map points
    /// </summary>
    public interface IConvertDirectoryToVoxelPoints
    {
        /// <summary>
        /// Converts the sheet format to Voxel Map points
        /// </summary>
        /// <param name="pointFileInSheetFormat">A file containing a copy paste from the directory</param>
        /// <param name="prefixToInclude">The prefix to include on the line. This ensures you can recognise the lines again.</param>
        /// <returns>A list of <see cref="string"/>s which are in the Voxel Map format</returns>
        List<string> ConvertDirectorySheetToVoxelPoints(string pointFileInSheetFormat, string prefixToInclude);
    }
}
