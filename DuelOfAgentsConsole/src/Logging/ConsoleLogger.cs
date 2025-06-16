using System.Collections.Specialized;
using DuelOfAgentsConsole.Llm;

namespace DuelOfAgentsConsole.Logging
{
    public class ConsoleLogger
    {
        private string firstRole = "";

        public ConsoleLogger()
        {

        }
        
        public void LogRoundTitle(int roundNumber)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"\n\n------ Round {roundNumber} ------");
        }

        public void LogClosingMessage()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\nDebate Ended");
        }

        public void LogRoundArguments(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems == null)
            {
                return;
            }

            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var message = e.NewItems[0] as CustomChatMessage;
                if (firstRole == "")
                {
                    firstRole = message.Role;
                }
                ;
                var color = firstRole == message.Role ? ConsoleColor.Cyan : ConsoleColor.Yellow;
                Console.ForegroundColor = color;
                Console.WriteLine($"\n{message.Role} argues:");
                Console.WriteLine(message.Content);
            }
        }
    }
}