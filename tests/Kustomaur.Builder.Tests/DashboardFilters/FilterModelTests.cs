using System;
using System.Collections.Generic;
using Kustomaur.Models.Filters;
using Kustomaur.Dashboard.DashboardParts.Implementations.SubParts;
using Xunit;

namespace Kustomaur.Builder.Tests.DashboardFilters
{
    public class FilterModelTests
    {
        [Fact]
        public void FilterModel_PropertiesAreSetCorrectly()
        {
            // Arrange
            var filterOperator = FilterOperator.Equals;
            var values = new List<string> { "value1", "value2" };
            var filterModel = new FilterModel("EntityName", FilterOperator.Equals, new List<string>()
            {
                "value1",
                "value2"
            });

            // Act
            var actualOperator = FilterOperator.Equals;
            var actualValues = filterModel.Values;

            // Assert
            Assert.Equal(filterOperator, actualOperator);
            Assert.Equal(values, actualValues);
        }
    }
}