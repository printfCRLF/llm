using DuelOfAgentsConsole.Llm;
using OpenAI.Chat;

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

        public override async Task<string> PresentArgumentAsync(string debateTopic, List<CustomChatMessage> conversation)
        {
            var messages = ConstructMessagesWithContext(debateTopic, conversation);
            var options = new ChatCompletionOptions
            {
                MaxOutputTokenCount = 100, // You can adjust the max output tokens as needed
                Temperature = Temperature
            };
            (string response, int tokenCount) = await ChatService.GetChatCompletionAsync(messages, options);
            return response;
        }
    }
}