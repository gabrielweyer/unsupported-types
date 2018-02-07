# Azure Table storage Unsupported Types

| Package | Release | Pre-release |
| --- | --- | --- |
| `TableStorage.UnsupportedTypes` | [![NuGet][nuget-badge]][nuget] | [![MyGet][myget-badge]][myget] |

| CI | Status | Platform(s) | Framework(s) | Test Framework(s) |
| --- | --- | --- | --- | --- |
| [AppVeyor][app-veyor] | [![Build Status][app-veyor-shield]][app-veyor] | `Windows` | `nestandard2.0` | `netcoreapp2.0.5` |

[Azure Table storage][table-storage] supports a [limited set of data types][supported-types] (namely `byte[]`, `bool`, `DateTime`, `double`, `Guid`, `int`, `long` and `string`). This `NuGet` package allows to store unsupported data type with some limitations:

- Your `Type` should be serializable / deserializable to and from `JSON` using [Json.NET][json-net]
- The entity should [fit][property-limitations] in `1 MB`

## How it works

1. Your `TableEntity` should inherit from `UnsupportedTypesTableEntity`
1. Decorate the property you want to store with the `UnsupportedTypeAttribute`
1. That's all

```csharp
public class MyCustomClass
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}

public class MyTableEntity : UnsupportedTypesTableEntity
{
    [UnsupportedType]
    public MyCustomClass SomeProperty { get; set; }
}
```

## Potential improvements

### Cache properties

Each read and write to `Azure Table storage` will trigger the use of `Reflection`. This could improved by caching the unsupported properties, in this case the scan would happen once per application lifetime.

[table-storage]: https://docs.microsoft.com/en-au/azure/cosmos-db/table-storage-overview
[supported-types]: https://docs.microsoft.com/en-us/rest/api/storageservices/understanding-the-table-service-data-model#property-types
[property-limitations]: https://docs.microsoft.com/en-us/rest/api/storageservices/understanding-the-table-service-data-model#property-limitations
[json-net]: https://www.newtonsoft.com/json
[nuget-badge]: https://img.shields.io/nuget/v/TableStorage.UnsupportedTypes.svg?label=NuGet
[nuget]: https://www.nuget.org/packages/TableStorage.UnsupportedTypes/
[myget-badge]: https://img.shields.io/myget/gabrielweyer-pre-release/v/TableStorage.UnsupportedTypes.svg?label=MyGet
[myget]: https://www.myget.org/feed/gabrielweyer-pre-release/package/nuget/TableStorage.UnsupportedTypes
[app-veyor]: https://ci.appveyor.com/project/GabrielWeyer/unsupported-types
[app-veyor-shield]: https://ci.appveyor.com/api/projects/status/github/gabrielweyer/unsupported-types?branch=master&svg=true
