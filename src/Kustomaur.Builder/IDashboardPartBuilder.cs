using System.Collections.Generic;
using Kustomaur.Dashboard.Implementation;
using Kustomaur.Models;

namespace Kustomaur.Dashboard
{
    public interface IDashboardPartBuilder : IBaseBuilder
    { 
        IDashboardPartBuilder AddPart(Part part);
        IDashboardPartBuilder AddPartsAsRow(List<Part> parts, out int maxYPos, int startYPos = 0);
        IDashboardPartBuilder AddPartsAsColumn(List<Part> parts, out int maxXPos, int startXPos = 0);
    }
}