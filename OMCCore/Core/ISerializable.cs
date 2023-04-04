namespace OMCCore.Core
{
    public interface ISerializable
    {
        public void Deserialize(string data);
        public string Serialize();
    }
}
