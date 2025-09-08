using System.Net;

namespace Task2.Services
{
    public class ApiResponse
    {
        public bool ISuccussed { get; set; }
        public HttpStatusCode StatusCodes { get; set; }
        public object? Result { get; set; }
        public List<String> Errors { get; set; }
    }
}
