namespace Transport.Data
{
    interface ISerializeLab
    {
        void SerializeJson(object type);
        void DeserializeJson(object type);
    }
}
