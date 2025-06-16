using DuelOfAgentsConsole.Agents;
using DuelOfAgentsConsole.Llm;

class DebateOrchestrator
{
    private string _topic;
    private int _numOfRounds;
    private readonly Agent _agent1;
    private readonly Agent _agent2;

    private readonly List<CustomChatMessage> _conversation = new List<CustomChatMessage>();

    public DebateOrchestrator(Agent agent1, Agent agent2)
    {
        _agent1 = agent1;
        _agent2 = agent2;
    }
    

    public void InitializeDebate(string topic, int numOfRounds)
    {
        _topic = topic;
        _numOfRounds = numOfRounds;
        _conversation.Clear();

        Console.WriteLine($"Debate Topic: {_topic}");
        Console.WriteLine($"Number of Rounds: {_numOfRounds}");
        Console.WriteLine($"Participants:\t{_agent1.Name} vs. {_agent2.Name}");
        
    }

    public async Task StartDebateAsync()
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("\n\n------ Round 1 ------");

        // First round
        var argument1 = await _agent1.PresentArgumentAsync(_topic, _conversation);
        _conversation.Add(new CustomChatMessage(_agent1.Name, argument1));
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"\n{_agent1.Name} argues:");
        Console.WriteLine(argument1);

        var argument2 = await _agent2.PresentArgumentAsync(argument1, _conversation);
        _conversation.Add(new CustomChatMessage(_agent2.Name, argument2));
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"\n{_agent2.Name} argues:");
        Console.WriteLine(argument2);


        // Following rounds
        for (int i = 1; i < _numOfRounds; i++)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"\n\n------ Round {i + 1} ------");
            
            var lastArgument = _conversation.Last().Content;
            argument1 = await _agent1.PresentArgumentAsync(lastArgument, _conversation);
            _conversation.Add(new CustomChatMessage(_agent1.Name, argument1));
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n{_agent1.Name} argues:");
            Console.WriteLine(argument1);

            argument2 = await _agent2.PresentArgumentAsync(argument1, _conversation);
            _conversation.Add(new CustomChatMessage(_agent2.Name, argument2));
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n{_agent2.Name} argues:");
            Console.WriteLine(argument2);

        }

        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("\nDebate Ended");
    }

}