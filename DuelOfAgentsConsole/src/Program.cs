using OpenAI.Chat;
using Microsoft.Extensions.Configuration;
using DuelOfAgentsConsole.Llm;

namespace DuelOfAgentsConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {

            string? apiKey = Environment.GetEnvironmentVariable("openai_apikey");


            Console.WriteLine("Duel of Agents Console Application");

            
            ChatClient client = new(model: "gpt-4o-mini", apiKey: apiKey);
            ChatCompletion completion = await client.CompleteChatAsync("When and where was Voyado founded?");
            Console.WriteLine($"[ASSISTANT]: {completion.Content[0].Text}");

            ChatService chatService = new ChatService(apiKey, "gpt-4o-mini");
            Agent pirate = new Agent(chatService, "Pirate", "Boisterous");
            Agent monk = new Agent(chatService, "Monk", "Buddhist");

            DebateOrchestrator orchestrator = new DebateOrchestrator(pirate, monk);

            orchestrator.InitializeDebate(
                "Can a pirate and a monk agree on the best way to achieve inner peace?",
                3
            );
            
            try 
            {
                await orchestrator.StartDebateAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred during the debate: {ex.Message}");
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}