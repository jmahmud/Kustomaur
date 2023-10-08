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
            var filterOperator = FilterOperator.equals.ToString();
            var values = new List<string> { "value1", "value2" };
            var filterModel = new FilterModel
            {
                Operator = filterOperator,
                Values = values
            };

            // Act
            var actualOperator = Enum.Parse<FilterOperator>(filterModel.Operator);
            var actualValues = filterModel.Values;

            // Assert
            Assert.Equal(filterOperator, actualOperator.ToString());
            Assert.Equal(values, actualValues);
        }
    }
}