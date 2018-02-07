using System.Collections.Generic;
using FluentAssertions;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Xunit;

namespace TableStorage.UnsupportedTypes.Tests
{
    public class UnsupportedTypesTableEntityTests
    {
        [Fact]
        public void GivenUnsupportedProperty_WhenWrite_ThenSerializeUnsupportedPropertyAsJson()
        {
            // Arrange

            var entity = new TestTableEntity
            {
                SomeProperty = new MyCustomClass
                {
                    FirstName = "Tom",
                    LastName = "Hardy"
                }
            };

            // Act

            var actualProperties = entity.WriteEntity(new OperationContext());

            // Assert

            const string expectedUnsupportedProperty = "{\"FirstName\":\"Tom\",\"LastName\":\"Hardy\"}";

            Assert.True(actualProperties.ContainsKey("SomeProperty"));
            Assert.Equal(expectedUnsupportedProperty, actualProperties["SomeProperty"].StringValue);
        }

        [Fact]
        public void GivenPropertiesWithUnsupportedProperty_WhenRead_ThenSetUnsupportedProperty()
        {
            // Arrange

            var properties = new Dictionary<string, EntityProperty>
            {
                { "SomeProperty", new EntityProperty("{\"FirstName\":\"Tom\",\"LastName\":\"Hardy\"}") }
            };

            var actual = new TestTableEntity();

            // Act

            actual.ReadEntity(properties, new OperationContext());

            // Assert

            var expected = new MyCustomClass
            {
                FirstName = "Tom",
                LastName = "Hardy"
            };

            actual.SomeProperty.Should().BeEquivalentTo(expected);
        }

        private class TestTableEntity : UnsupportedTypesTableEntity
        {
            [UnsupportedType]
            public MyCustomClass SomeProperty { get; set; }
        }

        private class MyCustomClass
        {
            // ReSharper disable UnusedAutoPropertyAccessor.Local
            public string FirstName { get; set; }
            public string LastName { get; set; }
            // ReSharper restore UnusedAutoPropertyAccessor.Local
        }
    }
}
