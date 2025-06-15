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
    }
}