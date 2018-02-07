namespace TableStorage.UnsupportedTypes.SampleConsole.Storage
{
    public class UnsupportedTypesTestTableEntity : UnsupportedTypesTableEntity
    {
        [UnsupportedType]
        public Unimportant VeryImportant { get; set; }
    }

    public class Unimportant
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
