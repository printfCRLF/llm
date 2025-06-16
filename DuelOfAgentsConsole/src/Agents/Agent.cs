using DuelOfAgentsConsole.Llm;
using OpenAI.Chat;

namespace DuelOfAgentsConsole.Agents
{
    public abstract class Agent
    {
        public string Name { get; set; }
        protected string Persona { get; set; }
        protected float Temperature { get; set; }
        protected ChatService ChatService { get; set; }

        public Agent(string name, string persona, float temperature, ChatService chatService)
        {
            Name = name;
            Persona = persona;
            Temperature = temperature;
            ChatService = chatService;
        }

        public abstract Task<string> PresentArgumentAsync(string debateTopic, List<CustomChatMessage> conversation);

        protected List<CustomChatMessage> ConstructMessagesWithContext(string debateTopic, List<CustomChatMessage> conversation)
        {
            var messages = new List<CustomChatMessage>
            {
                new CustomChatMessage("system", Persona),
                new CustomChatMessage("user", $"Debate Topic: {debateTopic}. Please limit your arguments to 100 words."),
            };
        
            // Add the current conversation history, ensuring roles are appropriate for context
            foreach (var msg in conversation)
            {
                // If the message is from the *other* agent, it should appear as 'user' to the current agent.
                // If it's from *this* agent, it should appear as 'assistant'.
                if (msg.Role == Name) // This agent's previous messages
                {
                    messages.Add(new CustomChatMessage("assistant", msg.Content));
                }
                else if (msg.Role != "system" && msg.Role != "user") // Other agent's messages
                {
                    messages.Add(new CustomChatMessage("user", msg.Content));
                }
                else // Original debate topic or system messages
                {
                    messages.Add(msg);
                }
            }
            return messages;
        }

        public override string ToString()
        {
            return $"{Name} ({Persona})";
        }

    }
}