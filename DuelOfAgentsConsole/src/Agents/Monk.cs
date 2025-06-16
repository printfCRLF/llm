using DuelOfAgentsConsole.Llm;
using OpenAI.Chat;

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