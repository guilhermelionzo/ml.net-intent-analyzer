namespace IntentAnalysis.Domain.Commands
{
    public class CommandResult
    {
        public CommandResult(bool success, string message, dynamic data)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        public bool Success { get; set; }
        public string Message { get; set; }
        public dynamic Data { get; set; }
    }
}