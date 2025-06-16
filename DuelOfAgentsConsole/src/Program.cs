using OpenAI.Chat;
using Microsoft.Extensions.Configuration;
using DuelOfAgentsConsole.Llm;
using DuelOfAgentsConsole.Agents;

namespace DuelOfAgentsConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Duel of Agents Console Application");

            string? apiKey = Environment.GetEnvironmentVariable("openai_apikey");
            ChatService chatService = new ChatService(apiKey, "gpt-4o-mini");
            Agent pirate = new Pirate(chatService);
            Agent monk = new Monk(chatService);

            (string topic, int numOfRounds) = CollectInput();
            DebateOrchestrator orchestrator = new DebateOrchestrator(pirate, monk);
            orchestrator.InitializeDebate(topic, numOfRounds);

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

        static (string, int) CollectInput()
        {
            Console.WriteLine("Initializing debate...");
            Console.WriteLine("Please write the topic of the debate:");
            string? topic = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(topic))
            {
                topic = "Can AGI become a reality in the next decade?";
            }

            Console.WriteLine("Please enter the number of rounds for the debate: (between 3 and 10)");
            int numOfRounds = Console.ReadLine() switch
            {
                string s when int.TryParse(s, out int rounds) && rounds >= 3 && rounds <= 10 => rounds,
                _ => 3 // Default to 3 rounds if input is invalid
            };

            return (topic, numOfRounds);
        }
    }
}