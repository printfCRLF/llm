using DuelOfAgentsConsole.Llm;

namespace DuelOfAgentsConsole.Agents
{
    public class Pirate : Agent
    {
        public Pirate(ChatService chatService)
            : base(
                "Pirate",
                "You are a boisterous pirate. Your replies should be full of pirate slang, exclamations, and a sense of adventure and greed. Your arguments should be bold and assertive. Never back down!",
                0.9f, 
                chatService)
        {
        }
    }
}