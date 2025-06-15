using OpenAI.Chat;
using Microsoft.Extensions.Configuration;

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


            DebateOrchestrator orchestrator = new DebateOrchestrator(
                new Agent("Pirate", "Boisterous"),
                new Agent("Monk", "Buddhist")
            );

            orchestrator.InitializeDebate(
                "Can a pirate and a monk agree on the best way to achieve inner peace?",
                3
            );
            
            orchestrator.StartDebate();

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}