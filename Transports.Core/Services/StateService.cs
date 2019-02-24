namespace Transports.Core.Services
{
    public static class StateService
    {
        public static StoreType StoreType { get; private set; }
        public static SerializationProvider SerializationProvider { get; private set; }

        public static StoreType SetStoreType(StoreType type)
        {
            StoreType = type;
            return StoreType;
        }

        public static SerializationProvider SetSerializationProvider(SerializationProvider provider)
        {
            SerializationProvider = provider;
            return SerializationProvider;
        }
    }

    public enum StoreType
    {
        InMemory,
        InDatabase
    }

    public enum SerializationProvider
    {
        XmlSerializer,
        JsonSerializer
    }
}
