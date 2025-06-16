
namespace DuelOfAgentsConsole.Logging
{
    public class UsageLogger
    {
        private readonly List<int> _usageRecords = new();

        public void LogUsage(int tokenCount)
        {
            _usageRecords.Add(tokenCount);
        }
        
        public int TotalUsage
        {
            get
            {
                return _usageRecords.Sum();
            }
        }
    }
}