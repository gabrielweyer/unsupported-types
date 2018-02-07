using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;

namespace TableStorage.UnsupportedTypes
{
    public class UnsupportedTypesTableEntity : TableEntity
    {
        public override IDictionary<string, EntityProperty> WriteEntity(OperationContext operationContext)
        {
            var results = base.WriteEntity(operationContext);

            var unsupportedPropertiesProperties = GetUnsupportedProperties();

            foreach (var unsupportedProperty in unsupportedPropertiesProperties)
            {
                results.Add(
                    unsupportedProperty.Name,
                    new EntityProperty(JsonConvert.SerializeObject(unsupportedProperty.GetValue(this))));
            }

            return results;
        }

        public override void ReadEntity(
            IDictionary<string, EntityProperty> properties,
            OperationContext operationContext)
        {
            base.ReadEntity(properties, operationContext);

            var unsupportedProperties = GetUnsupportedProperties();

            foreach (var unsupportedProperty in unsupportedProperties)
            {
                var value = JsonConvert.DeserializeObject(
                    properties[unsupportedProperty.Name].StringValue,
                    unsupportedProperty.PropertyType);

                unsupportedProperty.SetValue(this, value);
            }
        }

        private IEnumerable<PropertyInfo> GetUnsupportedProperties()
        {
            var unsupportedProperties = GetType()
                .GetProperties()
                .Where(prop => Attribute.IsDefined(prop, typeof(UnsupportedTypeAttribute)));

            return unsupportedProperties;
        }
    }
}
