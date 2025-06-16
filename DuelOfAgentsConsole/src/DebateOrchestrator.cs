using System.Collections.ObjectModel;
using DuelOfAgentsConsole.Agents;
using DuelOfAgentsConsole.Llm;
using DuelOfAgentsConsole.Logging;

class DebateOrchestrator
{
    private string _topic;
    private int _numOfRounds;
    private readonly Agent _agent1;
    private readonly Agent _agent2;
    private readonly ConsoleLogger _cLogger;
    private readonly UsageLogger _usageLogger;

    private readonly ObservableCollection<CustomChatMessage> _conversation = [];

    public DebateOrchestrator(Agent agent1, Agent agent2, ConsoleLogger cLogger, UsageLogger usageLogger)
    {
        _agent1 = agent1;
        _agent2 = agent2;
        _cLogger = cLogger;
        _usageLogger = usageLogger;
    }


    public void InitializeDebate(string topic, int numOfRounds)
    {
        _topic = topic;
        _numOfRounds = numOfRounds;
        _conversation.Clear();
        _conversation.CollectionChanged += _cLogger.LogRoundArguments;

        Console.WriteLine($"Debate Topic: {_topic}");
        Console.WriteLine($"Number of Rounds: {_numOfRounds}");
        Console.WriteLine($"Participants:\t{_agent1.Name} vs. {_agent2.Name}");

    }

    public async Task StartDebateAsync()
    {
        // First round
        _cLogger.LogRoundTitle(1);
        (string argument1, int tokenCount) = await _agent1.PresentArgumentAsync(_topic, _conversation);
        _conversation.Add(new CustomChatMessage(_agent1.Name, argument1));
        _usageLogger.LogUsage(tokenCount);

        (string argument2, tokenCount) = await _agent2.PresentArgumentAsync(argument1, _conversation);
        _conversation.Add(new CustomChatMessage(_agent2.Name, argument2));
        _usageLogger.LogUsage(tokenCount);


        // Following rounds
        for (int i = 1; i < _numOfRounds; i++)
        {
            _cLogger.LogRoundTitle(i + 1);

            var lastArgument = _conversation.Last().Content;
            (argument1, tokenCount) = await _agent1.PresentArgumentAsync(lastArgument, _conversation);
            _conversation.Add(new CustomChatMessage(_agent1.Name, argument1));
            _usageLogger.LogUsage(tokenCount);


            (argument2, tokenCount) = await _agent2.PresentArgumentAsync(argument1, _conversation);
            _conversation.Add(new CustomChatMessage(_agent2.Name, argument2));
            _usageLogger.LogUsage(tokenCount);

        }
    }
    
    public void EndDebate()
    {
        _cLogger.LogClosingMessage();
        _conversation.CollectionChanged -= _cLogger.LogRoundArguments;
    }

}