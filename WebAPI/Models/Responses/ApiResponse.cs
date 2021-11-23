namespace WebAPI.Models.Responses
{
    public class ApiResponse<T>
    {
        public ApiResponse()
        {
            Status = new ResponseStatus();
        }

        public ResponseStatus Status { get; set; }
        public T Data { get; set; }

    }

    public class ApiResponse
    {
        public ApiResponse()
        {
            Status = new ResponseStatus();
        }

        public ResponseStatus Status { get; }
    }

    public class ResponseStatus
    {
        public int Code { get; set; }
        public string Message { get; set; }
    }
}
