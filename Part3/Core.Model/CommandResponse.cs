using System.Collections.Generic;
using System.Linq;

namespace Core.Model
{
    public class CommandResponse
    {
        protected CommandResponse()
        {
            Errors = new CommandError[0];
        }

        public bool IsSuccess => Errors.Count==0;

        public IReadOnlyCollection<CommandError> Errors { get; set; }

        public static CommandResponse Ok() {  return new CommandResponse();}

        public static CommandResponse WithError(string error)
        {
            return new CommandResponse
            {
                Errors = new []{new CommandError(error)}
            };
        }

        public static CommandResponse WithError(string key, string error)
        {
            return new CommandResponse
            {
                Errors = new[] { new CommandError(key, error) }
            };
        }

        public static CommandResponse WithErrors(IReadOnlyCollection<CommandError> errors)
        {
            return new CommandResponse
            {
                Errors = errors
            };
        }
    }

    public class CommandResponse<T> : CommandResponse
    {
        public T Result { get; set; }

        public static CommandResponse<T> Ok(T result) { return new CommandResponse<T> { Result = result}; }

        public new static CommandResponse<T> WithError(string error)
        {
            return new CommandResponse<T>
            {
                Errors = new []{new CommandError(error) }
            };
        }

        public new static CommandResponse<T> WithError(string key, string error)
        {
            return new CommandResponse<T>
            {
                Errors = new[] { new CommandError(key, error) }
            };
        }

        public new static CommandResponse<T> WithErrors(IReadOnlyCollection<CommandError> errors)
        {
            return new CommandResponse<T>
            {
                Errors = errors
            };
        }

        public static implicit operator T(CommandResponse<T> from)
        {
            return from.Result;
        }
    }
}
