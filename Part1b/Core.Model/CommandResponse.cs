namespace Core.Model
{
    public class CommandResponse
    {
        protected CommandResponse()
        {
            
        }

        public bool IsSuccess { get; set; }

        public string ErrorMessage { get; set; }

        public static CommandResponse Ok() {  return new CommandResponse { IsSuccess = true};}

        public static CommandResponse WithError(string error) {  return new CommandResponse { IsSuccess = false, ErrorMessage = error};}
    }

    public class CommandResponse<T> : CommandResponse
    {
        public T Result { get; set; }

        public static CommandResponse<T> Ok(T result) { return new CommandResponse<T> { IsSuccess = true, Result = result}; }

        public new static CommandResponse<T> WithError(string error) { return new CommandResponse<T> { IsSuccess = false, ErrorMessage = error }; }
    }
}
