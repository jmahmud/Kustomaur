using System.Collections.Generic;
using System.Linq;
using Kustomaur.Dashboard.DashboardParts;
using Kustomaur.Models;

namespace Kustomaur.Dashboard.Implementation
{
    public class DashboardPartsBuilder : IDashboardPartBuilder
    {
        private Parts _parts;

        private string _subscriptionId;

        private string _resourceGroup;

        public DashboardPartsBuilder()
        {
            _parts = new Parts();
        }
        
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

        public IDashboardPartBuilder AddPart(Part part)
        {
            _parts.WithPart(part);
            return this;
        }
        
        public IDashboardPartBuilder AddPart(DashboardPart dashboardPart)
        {
            // add subscriptionid & resourcegroup
            _parts.WithPart(dashboardPart.GeneratePart());
            return this;
        }
        
        public IDashboardPartBuilder AddPartsAsRow(List<Part> parts, out int maxYPos, int startYPos = 0)
        {
            Part previousPart = null;
            parts.ForEach(p =>
            {
                // Set the Y position
                p.Position.Y = startYPos;
                
                // Set the X position (based upon previous part)
                if (previousPart == null)
                {
                    previousPart = p;
                }
                else
                {
                    p.Position.X = previousPart.Position.X + previousPart.Position.ColSpan;
                }
                _parts.WithPart(p);
            });
            maxYPos = parts.Max(p => p.Position.RowSpan) + startYPos;
            return this;
        }

        private int _startYPosForNextRow = 0;
        public IDashboardPartBuilder AddPartsAsRow(List<Part> parts)
        {
            Part previousPart = null;
            parts.ForEach(p =>
            {
                // Set the Y position
                p.Position.Y = _startYPosForNextRow;
                
                // Set the X position (based upon previous part)
                if (previousPart == null)
                {
                    previousPart = p;
                }
                else
                {
                    p.Position.X = previousPart.Position.X + previousPart.Position.ColSpan;
                }
                _parts.WithPart(p);
            });
            _startYPosForNextRow = parts.Max(p => p.Position.RowSpan) + _startYPosForNextRow;
            return this;
        }
        public IDashboardPartBuilder AddPartsAsColumn(List<Part> parts, out int maxXPos, int startXPos = 0)
        {
            Part previousPart = null;
            parts.ForEach(p =>
            {
                // Set the X position
                p.Position.X = startXPos;
                
                // Set the Y position (based upon previous part)
                if (previousPart == null)
                {
                    previousPart = p;
                }
                else
                {
                    p.Position.Y = previousPart.Position.Y + previousPart.Position.RowSpan;
                }
                _parts.WithPart(p);
            });
            maxXPos = parts.Max(p => p.Position.ColSpan) + startXPos;
            return this;
        }
        
        private int _startXPosForNextColumn = 0;
        public IDashboardPartBuilder AddPartsAsColumn(List<Part> parts)
        {
            Part previousPart = null;
            parts.ForEach(p =>
            {
                // Set the X position
                p.Position.X = _startXPosForNextColumn;
                
                // Set the Y position (based upon previous part)
                if (previousPart == null)
                {
                    previousPart = p;
                }
                else
                {
                    p.Position.Y = previousPart.Position.Y + previousPart.Position.RowSpan;
                }
                _parts.WithPart(p);
            });
            _startXPosForNextColumn = parts.Max(p => p.Position.ColSpan) + _startXPosForNextColumn;
            return this;
        }
    }
}