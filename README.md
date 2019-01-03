# Azure Table storage Unsupported Types

| Package | Release | Pre-release |
| --- | --- | --- |
| `TableStorage.UnsupportedTypes` | [![NuGet][nuget-badge]][nuget] | [![MyGet][myget-badge]][myget] |

| CI | Status | Platform(s) | Framework(s) | Test Framework(s) |
| --- | --- | --- | --- | --- |
| [AppVeyor][app-veyor] | [![Build Status][app-veyor-shield]][app-veyor] | `Windows` | `nestandard2.0` | `netcoreapp2.2.0` |

[Azure Table storage][table-storage] supports a [limited set of data types][supported-types] (namely `byte[]`, `bool`, `DateTime`, `double`, `Guid`, `int`, `long` and `string`). `Unsupported Types` allows to store unsupported data types with some limitations:

- Your `Type` should be serializable / deserializable to and from `JSON` using [Json.NET][json-net]
- The entity should [fit][property-limitations] in `1 MB`

This is distributed via a `NuGet` package but the implementation is so simple that you can just copy the classes into your own solution if that works better for you.

## How it works

1. Your `TableEntity` should inherit from `UnsupportedTypesTableEntity`
1. Decorate the properties you want to store with the `UnsupportedTypeAttribute`
1. That's all

```csharp
public class Unimportant
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}

public class UnsupportedTypesTestTableEntity : UnsupportedTypesTableEntity
{
    [UnsupportedType]
    public Unimportant VeryImportant { get; set; }
}
```

There is a console application in [src/SampleConsole](src/SampleConsole) demonstrating `Unsupported Types`:

- You'll need the [Azure storage emulator][azure-storage-emulator] **5.5** or later
  - Alternatively you can use a [storage account][create-storage-account] and modify `AzureTableStorage:ConnectionString` accordingly in `appsettings.json`

### Output of the console

![Console][console-screenshot]

### Entity stored in storage

![Storage][storage-screenshot]

## Limitations

You will not be able to [filter][filter] the entities using the unsupported types. You'll need to materialize them first and then use [LINQ to Objects][linq-objects].

## Potential improvements

### Cache properties

Each read and write to `Azure Table storage` will trigger the use of `Reflection`. This could be improved by caching the unsupported properties, in this case the scan would happen once per application lifetime.

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
[linq-objects]: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/linq-to-objects
[filter]: https://docs.microsoft.com/en-us/rest/api/storageservices/querying-tables-and-entities#constructing-filter-strings
[azure-storage-emulator]: https://docs.microsoft.com/en-us/azure/storage/common/storage-use-emulator
[create-storage-account]: https://docs.microsoft.com/en-us/azure/storage/common/storage-quickstart-create-account?tabs=portal
[console-screenshot]: docs/console.png
[storage-screenshot]: docs/storage.png
