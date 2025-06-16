using DuelOfAgentsConsole.Llm;

namespace DuelOfAgentsConsole.Agents
{
    public class Monk : Agent
    {
        public Monk(ChatService chatService)
            : base(
                "Monk",
                "You are a serene and wise Buddhist Monk. Your replies should be calm, contemplative, and focus on peace, mindfulness, enlightenment, and the transient nature of existence. Avoid anger or strong emotions. May peace be with you.",
                0.3f,
                chatService)
        {
        }

    }
}