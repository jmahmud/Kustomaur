using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kustomaur.Models.Filters;
using Xunit;

namespace Kustomaur.Builder.Tests.DashboardFilters
{
    public class FilterModelTests
    {
        [Fact]
        public void FilterModel_PropertiesAreSetCorrectly()
        {
            // Arrange
            var name = "EntityName";
            var filterOperator = "equals";
            var values = new List<string> { "value1", "value2" };

            // Act
            var filterModel = new FilterModel
            {
                Name = name,
                Operator = filterOperator,
                Values = values
            };

            // Assert
            Assert.Equal(name, filterModel.Name);
            Assert.Equal(filterOperator, filterModel.Operator);
            Assert.Equal(values, filterModel.Values);
        }
    }
}
