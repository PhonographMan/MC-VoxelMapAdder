using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MapPoints.MapPoints
{
    /// <summary>
    /// Updates map points
    /// </summary>
    public class UpdateMapPoints : IUpdateMapPoints
    {
        /// <summary>
        /// Converts the Dominion Directory format to Voxel Map points
        /// </summary>
        private readonly IConvertDirectoryToVoxelPoints convertDirectoryToVoxelPoints;

        public UpdateMapPoints(IConvertDirectoryToVoxelPoints convertDirectoryToVoxelPoints)
        {
            this.convertDirectoryToVoxelPoints = convertDirectoryToVoxelPoints != null ? convertDirectoryToVoxelPoints : throw new ArgumentNullException($"{nameof(convertDirectoryToVoxelPoints)}");
        }

        /// <summary>
        /// Updates the points in the file with a given.
        /// </summary>
        /// <param name="prefixInUpdatePoints">What prefix to look for when removing old points from the existing file</param>
        /// <param name="mapfileToUpdate">The map file to update with new points</param>
        /// <param name="pointsToUpdate">The current/up to date points in the Directory Sheet format!</param>
        /// <param name="errors">Any errors occured during the course of the method</param>
        /// <returns><c>true</c> means this updated successfully.</returns>
        public bool UpdateFromFile(string prefixInUpdatePoints, string mapfileToUpdate, string pointsToUpdate, out List<string> errors)
        {
            bool haveUpdated = false;

            if (ArgumentsAreValid(prefixInUpdatePoints, mapfileToUpdate, pointsToUpdate, out errors))
            {
                var mapFileOut = new List<string>();

                // Extract everything from the existing file which doesn't have the prefix
                // In theory you could do an Enumerator here but it's still going through 
                // the elements so this should be fine.
                string[] mapFileToUpdateContents = File.ReadAllLines(mapfileToUpdate);
                foreach (string line in mapFileToUpdateContents)
                {
                    if(!line.Contains(prefixInUpdatePoints))
                    {
                        mapFileOut.Add(line);
                    }
                }

                // The format will be from the directory and we want to convert it into the correct format
                List<string> pointsToUpdateAsVoxelPoints = 
                    this.convertDirectoryToVoxelPoints.ConvertDirectorySheetToVoxelPoints(pointsToUpdate, prefixInUpdatePoints);

                if (pointsToUpdateAsVoxelPoints.Count == 0)
                {
                    errors.Add("There were no points generated to add.");
                }
                else
                {
                    // Add our new updated points
                    mapFileOut.AddRange(pointsToUpdateAsVoxelPoints);

                    // This will override the current contents which is fine
                    File.WriteAllLines(mapfileToUpdate, mapFileOut);

                    haveUpdated = true;
                }
            }

            return haveUpdated;
        }

        /// <summary>
        /// Test to see if we can even begin to run an update
        /// </summary>
        /// <param name="prefixInUpdatePoints"></param>
        /// <param name="mapfileToUpdate"></param>
        /// <param name="pointsToUpdate"></param>
        /// <param name="errors"></param>
        /// <returns></returns>
        private bool ArgumentsAreValid(string prefixInUpdatePoints, string mapfileToUpdate, string pointsToUpdate, out List<string> errors)
        {
            errors = new List<string>();
            bool areValidArguments = true;

            if (string.IsNullOrEmpty(prefixInUpdatePoints))
            {
                errors.Add($"Prefix cannot be empty or whitespace");
                areValidArguments = false;
            }
            else if (!File.Exists(mapfileToUpdate))
            {
                errors.Add($"File to update does not exist: {mapfileToUpdate}");
                areValidArguments = false;
            }
            else if (!File.Exists(pointsToUpdate))
            {
                errors.Add($"File with new points does not exist: {pointsToUpdate}");
                areValidArguments = false;
            }

            return areValidArguments;
        }
    }
}
