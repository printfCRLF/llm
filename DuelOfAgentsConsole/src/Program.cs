using DuelOfAgentsConsole.Llm;
using DuelOfAgentsConsole.Agents;
using DuelOfAgentsConsole.Logging;

namespace DuelOfAgentsConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Duel of Agents Console Application");

            string? apiKey = Environment.GetEnvironmentVariable("openai_apikey");
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                Console.WriteLine("Please set the environment variable 'openai_apikey' with your OpenAI API key.");
                return;
            }

            ChatService chatService = new(apiKey, "gpt-4o-mini");
            Agent pirate = new Pirate(chatService);
            Agent monk = new Monk(chatService);
            ConsoleLogger consoleLogger = new();
            UsageLogger usageLogger = new();

            (string topic, int numOfRounds) = new PromptCollector().Run();

            DebateOrchestrator orchestrator = new DebateOrchestrator(pirate, monk, consoleLogger, usageLogger);
            orchestrator.InitializeDebate(topic, numOfRounds);

            try
            {
                await orchestrator.StartDebateAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred during the debate: {ex.Message}");
            }

            orchestrator.EndDebate();
            Console.WriteLine($"Total Token Usage: {usageLogger.TotalUsage}");

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }


    }
}