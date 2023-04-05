namespace OMCC.Plugins.GameManager
{
    public class StringArgument : IArgument
    {
        public StringArgument(string value)
        {
            Value = value;
        }
        public string Value { get; set; }

        public virtual string[] ToString(MinecraftSettings settings) => new string[1] { Value };
    }
}
