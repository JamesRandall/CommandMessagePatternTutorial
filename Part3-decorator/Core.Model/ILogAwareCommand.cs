namespace Core.Model
{
    public interface ILogAwareCommand
    {
        string GetPreDispatchLogMessage();
        string GetPostDispatchLogMessage();
        string GetDispatchErrorLogMessage();
    }
}
