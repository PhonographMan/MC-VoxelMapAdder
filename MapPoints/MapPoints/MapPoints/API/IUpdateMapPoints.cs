using System;
using System.Collections.Generic;
using System.Text;

namespace MapPoints.MapPoints
{
    /// <summary>
    /// Updates map points
    /// </summary>
    public interface IUpdateMapPoints
    {
        /// <summary>
        /// Updates the points in the file with a given.
        /// </summary>
        /// <param name="prefixInUpdatePoints">What prefix to look for when removing old points from the existing file</param>
        /// <param name="mapfileToUpdate">The map file to update with new points</param>
        /// <param name="pointsToUpdate">The current/up to date points in the Directory Sheet format!</param>
        /// <param name="errors">Any errors occured during the course of the method</param>
        /// <returns><c>true</c> means this updated successfully.</returns>
        bool UpdateFromFile(string prefixInUpdatePoints, string mapfileToUpdate, string pointsToUpdate, out List<string> errors);
    }
}
