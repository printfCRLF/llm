namespace DuelOfAgentsConsole
{
    using System;


    public class PromptCollector
    {
        public (string topic, int numOfRounds) Run()
        {
            Console.WriteLine("Initializing debate...");

            string topic = collectTopic();
            int numOfRounds = NewMethod();

            return (topic, numOfRounds);
        }

        private static int NewMethod()
        {
            Console.WriteLine("Please enter the number of rounds for the debate: (between 3 and 10)");
            int numOfRounds = Console.ReadLine() switch
            {
                string s when int.TryParse(s, out int rounds) && rounds >= 3 && rounds <= 10 => rounds,
                _ => 3 // Default to 3 rounds if input is invalid
            };
            return numOfRounds;
        }

        private string collectTopic()
        {
            Func<int, int> tryAgain = (attemps) =>
            {
                Console.WriteLine("Invalid input. Please try again.");
                return ++attemps;
            };

            int attempts = 0;
            string topic = "";
            while (attempts < 3)
            {
                Console.WriteLine("Please write the topic of the debate:");
                topic = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(topic) || string.IsNullOrEmpty(topic))
                {
                    attempts = tryAgain(attempts);
                    continue;
                }
                if (hasHtmlTags(topic) || hasEscapeCharacters(topic))
                {
                    attempts = tryAgain(attempts);
                    continue;
                }
                break;
            }
            if (attempts >= 3)
            {
                topic = "Can AGI become a reality in the next decade?";       
            }
            
            return topic;
        }

        private bool hasHtmlTags(string input)
        {
            if (input.Contains("<") && input.Contains("/>"))
            {
                Console.WriteLine("Input contains HTML tags. Please enter a valid topic without HTML tags.");
                return true;
            }
            return false;
        }

        private bool hasEscapeCharacters(string input)
        {
            if (input.Contains("\n") || input.Contains("\r") || input.Contains("\t"))
            {
                Console.WriteLine("Input contains escape characters. Please enter a valid topic without escape characters.");
                return true;
            }
            if (input.Contains("\\") || input.Contains("/"))
            {
                Console.WriteLine("Input contains escape characters. Please enter a valid topic without escape characters.");
                return true;
            }
            return false;
        }
    }
    
}