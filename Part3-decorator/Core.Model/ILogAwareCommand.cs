using System;

namespace Core.Model
{
    public interface ILogAwareCommand
    {
        string GetPreDispatchLogMessage();
        string GetPostDispatchLogMessage();
        string GetDispatchErrorLogMessage(Exception ex);
    }
}
