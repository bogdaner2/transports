namespace Transports.Desktop
{
    public static class StateService
    {
        public static StoreType StoreType { get; private set; }
        public static WayDataLoad WayDataLoad { get; private set; }

        public static StoreType SetStoreType(StoreType type)
        {
            StoreType = type;
            return StoreType;
        }

        public static WayDataLoad SetWayForDataLoad(WayDataLoad way)
        {
            WayDataLoad = way;
            return WayDataLoad;
        }
    }

    public enum StoreType
    {
        InMemory,
        InDatabase
    }

    public enum WayDataLoad
    {
        Serialization,
        FromDatabase
    }
}
