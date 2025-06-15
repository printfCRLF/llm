using DuelOfAgentsConsole.Llm;

public class Agent
{
    public string Name { get; set; }
    protected string Persona { get; set; }
    protected double Temperature { get; set; }
    protected ChatService ChatService { get; set; }

    public Agent(ChatService chatService, string name, string persona, double temperature = 0.75)
    {
        Name = name;
        Persona = persona;
        Temperature = temperature;
        ChatService = chatService;
    }

    public async Task<string> PresentArgumentAsync(string argument)
    {
        return await ChatService.GetResponseAsync(argument);
    }


    public override string ToString()
    {
        return $"{Name} ({Persona})";
    }

}