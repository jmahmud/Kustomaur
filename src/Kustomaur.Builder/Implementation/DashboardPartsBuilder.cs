using System.Collections.Generic;
using System.Linq;
using Kustomaur.Dashboard.DashboardParts;
using Kustomaur.Models;

namespace Kustomaur.Dashboard.Implementation
{
    /// <summary>
    /// Builder to add <see cref="DashboardParts.DashboardPart"/> to a dashboard
    /// </summary>
    public class DashboardPartsBuilder : IDashboardPartBuilder
    {
        private Parts _parts;

        private string _subscriptionId;

        private string _resourceGroup;

        public DashboardPartsBuilder()
        {
            _parts = new Parts();
        }
        
        /// <summary>
        /// Build will be called by the <see cref="DashboardBuilder"/> and should update the passed in <see cref="Models.Dashboard"/> object
        /// </summary>
        /// <param name="dashboard"></param>
        public void Build(Models.Dashboard dashboard)
        {
            if (dashboard.Properties.Lenses == null)
            {
                dashboard.Properties.WithLenses(new Lenses());
                dashboard.Properties.Lenses.WithLense(new Lense());
            }
            
            dashboard.Properties.Lenses[0].WithParts(_parts);
        }

        public void WithSubscription(string subscriptionId)
        {
            _subscriptionId = subscriptionId;
        }

        public void WithResourceGroup(string resourceGroup)
        {
            _resourceGroup = resourceGroup;
        }

        /// <summary>
        /// Adds a part to the builder's dashboard
        /// </summary>
        /// <param name="part"></param>
        /// <returns></returns>
        public IDashboardPartBuilder AddPart(Part part)
        {
            _parts.WithPart(part);
            return this;
        }
        /// <summary>
        /// Adds a part to the builder's dashboard - will call <see cref="DashboardPart.GeneratePart"/> to produce <see cref="Part"/> 
        /// </summary>
        /// <param name="dashboardPart"></param>
        /// <returns></returns>
        public IDashboardPartBuilder AddPart(DashboardPart dashboardPart)
        {
            // add subscriptionid & resourcegroup
            _parts.WithPart(dashboardPart.GeneratePart());
            return this;
        }
        
        /// <summary>
        /// Adds a list of parts to the same row and calculates the part with longest length, in order to be used to compute the next row starting position
        /// </summary>
        /// <param name="parts">The parts to add in the same row</param>
        /// <param name="maxYPos">This is the out parameter which is set to specify the longest part in the row.</param>
        /// <param name="startYPos">Defaults to 0.  Can be used to set the starting Y position for the row.  This can be set to a previous row's maxYPos</param>
        /// <returns></returns>
        public IDashboardPartBuilder AddPartsAsRow(List<Part> parts, out int maxYPos, int startYPos = 0)
        {
            Part previousPart = null;
            parts.ForEach(p =>
            {
                // Set the Y position
                p.Position.Y = startYPos;

                if (previousPart != null)
                {
                    // Set the X position (based upon previous part)
                    p.Position.X = previousPart.Position.X + previousPart.Position.ColSpan;
                }
                _parts.WithPart(p);

                previousPart = p;
                
            });
            maxYPos = parts.Max(p => p.Position.RowSpan) + startYPos;
            return this;
        }

        private int _startYPosForNextRow = 0;
        /// <summary>
        /// Adds a list of parts to the same row.  Every call to this will add a new row.
        /// </summary>
        /// <param name="parts">The parts to add in the same row</param>
        public IDashboardPartBuilder AddPartsAsRow(List<Part> parts)
        {
            Part previousPart = null;
            parts.ForEach(p =>
            {
                // Set the Y position
                p.Position.Y = _startYPosForNextRow;
                
                if (previousPart != null)
                {
                    // Set the X position (based upon previous part)
                    p.Position.X = previousPart.Position.X + previousPart.Position.ColSpan;
                }
                _parts.WithPart(p);

                previousPart = p;
            });
            _startYPosForNextRow = parts.Max(p => p.Position.RowSpan) + _startYPosForNextRow;
            return this;
        }
        /// <summary>
        /// Adds a list of parts to the same column and calculates the part with longest width, in order to be used to compute the next column starting position
        /// </summary>
        /// <param name="parts">The parts to add in the same column</param>
        /// <param name="maxXPos">This is the out parameter which is set to specify the widest part in the column.</param>
        /// <param name="startXPos">Defaults to 0.  Can be used to set the starting X position for the column.  This can be set to a previous column's maxXPos</param>
        /// <returns></returns>
        public IDashboardPartBuilder AddPartsAsColumn(List<Part> parts, out int maxXPos, int startXPos = 0)
        {
            Part previousPart = null;
            parts.ForEach(p =>
            {
                // Set the X position
                p.Position.X = startXPos;
                
                if (previousPart != null)
                {
                    // Set the Y position (based upon previous part)
                    p.Position.Y = previousPart.Position.Y + previousPart.Position.RowSpan;
                }

                _parts.WithPart(p);
                
                previousPart = p;

            });
            maxXPos = parts.Max(p => p.Position.ColSpan) + startXPos;
            return this;
        }
        
        private int _startXPosForNextColumn = 0;
        /// <summary>
        /// Adds a list of parts to the same column. Every call to this will add a new column.
        /// </summary>
        public IDashboardPartBuilder AddPartsAsColumn(List<Part> parts)
        {
            Part previousPart = null;
            parts.ForEach(p =>
            {
                // Set the X position
                p.Position.X = _startXPosForNextColumn;
                
                if (previousPart != null)
                {
                    // Set the Y position (based upon previous part)
                    p.Position.Y = previousPart.Position.Y + previousPart.Position.RowSpan;
                }

                _parts.WithPart(p);
                
                previousPart = p;

            });
            _startXPosForNextColumn = parts.Max(p => p.Position.ColSpan) + _startXPosForNextColumn;
            return this;
        }
    }
}