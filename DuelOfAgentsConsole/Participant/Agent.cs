public class Agent
{
    public string Name { get; set; }
    public string Persona { get; set; }

    public Agent(string name, string persona)
    {
        Name = name;
        Persona = persona;
    }

    public string Speak(string input)
    {
        string output = "my response to " + input + " as $asdf";
        return output;
    }

    public override string ToString()
    {
        return $"{Name} ({Persona})";
    }

}