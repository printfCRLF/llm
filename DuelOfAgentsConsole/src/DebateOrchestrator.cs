class DebateOrchestrator
{
    private string _topic;
    private int _numOfRounds;

    private readonly Agent _agent1;
    private readonly Agent _agent2;

    private readonly List<string> _arguments = new List<string>();

    public DebateOrchestrator(Agent agent1, Agent agent2)
    {
        _agent1 = agent1;
        _agent2 = agent2;
    }
    

    public void InitializeDebate(string topic, int numOfRounds)
    {
        _topic = topic;
        _numOfRounds = numOfRounds;

        Console.WriteLine($"Debate Topic: {_topic}");
        Console.WriteLine($"Number of Rounds: {_numOfRounds}");
        Console.WriteLine("Participants:");
        Console.WriteLine($"  {_agent1} vs. {_agent2}");
        
    }

    public async Task StartDebateAsync()
    {
        // First round
        var argument1 = await _agent1.PresentArgumentAsync(_topic);
        _arguments.Add(argument1);
        Console.WriteLine($"Round 1: {_agent1.Name} argues: {argument1}");

        var argument2 = await _agent2.PresentArgumentAsync(argument1);
        _arguments.Add(argument2);
        Console.WriteLine($"Round 1: {_agent2.Name} argues: {argument2}");

        // Following rounds
        for (int i = 1; i < _numOfRounds; i++)
        {
            var lastArgument = _arguments.Last();
            argument1 = await _agent1.PresentArgumentAsync(lastArgument);
            Console.WriteLine($"Round {i + 1}: {_agent1.Name} argues: {argument1}");
            _arguments.Add(argument1);
            argument2 = await _agent2.PresentArgumentAsync(argument1);
            _arguments.Add(argument2);
            Console.WriteLine($"Round {i + 1}: {_agent2.Name} argues: {argument2}");
        }

        Console.WriteLine("Debate Ended");
    }

}