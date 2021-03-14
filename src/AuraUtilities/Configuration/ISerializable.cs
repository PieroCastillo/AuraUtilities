namespace AuraUtilities.Configuration
{
    public interface ISerializable<TNoSerializableObject>
    {
        public TNoSerializableObject CovertToNormal();
    }
}