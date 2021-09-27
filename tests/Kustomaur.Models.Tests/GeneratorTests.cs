using System;
using Newtonsoft.Json;
using Xunit;
using JsonSerializer = System.Text.Json.JsonSerializer;
using Kustomaur.Dashboard;

namespace Kustomaur.Models.Tests
{
    public class GeneratorTests
    {
        [Fact]
        public void CanAddLensesToProperties()
        {
            // Arrange
            var lenses = new Lenses();

            // Act
            var properties = new DashboardProperties()
                .WithLenses(lenses);
            
            // Assert
            var serialized = Kustomaur.Dashboard.Generator.Generate(properties);

            Assert.Contains("{\"lenses\":{},\"metadata\":null}", serialized);
        }
        
        [Fact]
        public void CanAddLenseToLenses()
        {
            // Arrange
            var lense1 = new Lense();
            var lense2 = new Lense();
            
            var lenses = new Lenses();
            lenses.WithLense(lense1);
            lenses.WithLense(lense2);

            // Act
            var properties = new DashboardProperties()
                .WithLenses(lenses);
            
            // Assert
            var serialized = Generator.Generate(properties);

            Assert.Contains("{\"lenses\":{\"0\":{\"order\":0,\"parts\":null},\"1\":{\"order\":0,\"parts\":null}},\"metadata\":null}", serialized);
        }
        
        [Fact]
        public void CanAddPartToParts()
        {
            // Arrange
            var part1 = new Part();
            var part2 = new Part();
            var parts = new Parts()
                .WithPart(part1)
                .WithPart(part2);
            
            var lense1 = new Lense()
                .WithParts(parts);

            var lenses = new Lenses();
            lenses.WithLense(lense1);

            // Act
            var properties = new DashboardProperties()
                .WithLenses(lenses);
            
            // Assert
            var serialized = Generator.Generate(properties);

            Assert.Contains("{\"lenses\":{\"0\":{\"order\":0,\"parts\":{\"0\":{\"position\":null,\"metadata\":null},\"1\":{\"position\":null,\"metadata\":null}}}},\"metadata\":null}", serialized);
        }
        
        [Fact]
        public void PartsHasPositionWithRawParameters()
        {
            // Arrange
            var part1 = new Part()
                .WithPosition(x:0, y:0, rowSpan:2, colSpan:1);
                
            var parts = new Parts()
                .WithPart(part1);
            
            var lense1 = new Lense()
                .WithParts(parts);

            var lenses = new Lenses();
            lenses.WithLense(lense1);

            // Act
            var properties = new DashboardProperties()
                .WithLenses(lenses);
            
            // Assert
            var serialized = Generator.Generate(properties);

            Assert.Contains("{\"lenses\":{\"0\":{\"order\":0,\"parts\":{\"0\":{\"position\":{\"x\":0,\"y\":0,\"colSpan\":1,\"rowSpan\":2},\"metadata\":null}}}},\"metadata\":null}", serialized);
        }
        
        [Fact]
        public void PartsHasPosition()
        {
            // Arrange
            var position = new Position(0, 0, 2, 1);
            var part1 = new Part()
                .WithPosition(position);
                
            var parts = new Parts()
                .WithPart(part1);
            
            var lense1 = new Lense()
                .WithParts(parts);

            var lenses = new Lenses();
            lenses.WithLense(lense1);

            // Act
            var properties = new DashboardProperties()
                .WithLenses(lenses);
            
            // Assert
            var serialized = Generator.Generate(properties);

            Assert.Contains("{\"lenses\":{\"0\":{\"order\":0,\"parts\":{\"0\":{\"position\":{\"x\":0,\"y\":0,\"colSpan\":1,\"rowSpan\":2},\"metadata\":null}}}},\"metadata\":null}", serialized);
        }
    }
}