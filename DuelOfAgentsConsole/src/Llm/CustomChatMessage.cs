namespace DuelOfAgentsConsole.Llm
{
    public class CustomChatMessage(string role, string content)
    {
        public string Role { get; set; } = role;

        public string Content { get; set; } = content;
    }
}