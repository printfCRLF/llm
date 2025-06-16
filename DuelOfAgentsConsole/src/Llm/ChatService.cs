using OpenAI.Chat;

namespace DuelOfAgentsConsole.Llm
{
    public class ChatService
    {
        private readonly ChatClient _client;

        public ChatService(string apiKey, string model = "gpt-4o-mini")
        {
            _client = new ChatClient(model, apiKey);
        }

        public async Task<string> GetResponseAsync(string prompt)
        {
            ChatCompletion completion = await _client.CompleteChatAsync(prompt);
            return completion.Content[0].Text;
        }

        public async Task<(string Reply, int tokenCount)> GetChatCompletionAsync(List<CustomChatMessage> customMessages, ChatCompletionOptions options)
        {
            var messages = new List<ChatMessage>();
            foreach (var cMsg in customMessages)
            {
                if (cMsg.Role == "system")
                {
                    messages.Add(new SystemChatMessage(cMsg.Content));
                }
                else if (cMsg.Role == "user")
                {
                    messages.Add(new UserChatMessage(cMsg.Content));
                }
                else if (cMsg.Role == "assistant")
                {
                    messages.Add(new AssistantChatMessage(cMsg.Content));
                }
            }

            ChatCompletion completion = await _client.CompleteChatAsync(messages, options);
            var reply = completion.Content[0].Text;
            var tokenCount = completion.Usage.TotalTokenCount;

            return (reply, tokenCount);
        }
    }
}