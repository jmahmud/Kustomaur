using System.Collections.Generic;
using Kustomaur.Dashboard.Implementation;
using Kustomaur.Models;

namespace Kustomaur.Dashboard
{
    /// <summary>
    /// Implement this in order to create a builder to add <see cref="DashboardParts.DashboardPart"/> to a dashboard
    /// </summary>
    public interface IDashboardPartBuilder : IBaseDashboardBuilder
    { 
        /// <summary>
        /// Adds a part to the builder's dashboard
        /// </summary>
        /// <param name="part"></param>
        /// <returns></returns>
        IDashboardPartBuilder AddPart(Part part);
        /// <summary>
        /// Adds a list of parts to the same row and calculates the part with longest length, in order to be used to compute the next row starting position
        /// </summary>
        /// <param name="parts">The parts to add in the same row</param>
        /// <param name="maxYPos">This is the out parameter which is set to specify the longest part in the row.</param>
        /// <param name="startYPos">Defaults to 0.  Can be used to set the starting Y position for the row.  This can be set to a previous row's maxYPos</param>
        /// <returns></returns>
        IDashboardPartBuilder AddPartsAsRow(List<Part> parts, out int maxYPos, int startYPos = 0);
        
        /// <summary>
        /// Adds a list of parts to the same row.  Every call to this will add a new row.
        /// </summary>
        /// <param name="parts">The parts to add in the same row</param>
        IDashboardPartBuilder AddPartsAsRow(List<Part> parts);
        /// <summary>
        /// Adds a list of parts to the same column and calculates the part with longest width, in order to be used to compute the next column starting position
        /// </summary>
        /// <param name="parts">The parts to add in the same column</param>
        /// <param name="maxXPos">This is the out parameter which is set to specify the widest part in the column.</param>
        /// <param name="startXPos">Defaults to 0.  Can be used to set the starting X position for the column.  This can be set to a previous column's maxXPos</param>
        /// <returns></returns>
        IDashboardPartBuilder AddPartsAsColumn(List<Part> parts, out int maxXPos, int startXPos = 0);
        /// <summary>
        /// Adds a list of parts to the same column. Every call to this will add a new column.
        /// </summary>
        IDashboardPartBuilder AddPartsAsColumn(List<Part> parts);

    }
}