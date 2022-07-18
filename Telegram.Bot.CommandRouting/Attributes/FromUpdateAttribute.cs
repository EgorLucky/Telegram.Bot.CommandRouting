namespace Telegram.Bot.CommandRouting.Attributes
{
    public class FromUpdateAttribute : Attribute
    {
        public FromUpdateAttribute(params string[] path)
        {
            Path = path;
        }

        public string[] Path { get; set; }
    }
}