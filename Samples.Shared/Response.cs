using System.Net;

namespace Samples.Shared
{
    public class Response
    {
        private readonly object _content;

        public Response()
            : this(HttpStatusCode.OK, "Success")
        { }

        public Response(HttpStatusCode code, string message)            
        {
            StatusCode = code;
            Message = message;            
        }

        public Response(object content)
            : this()
        {
            _content = content;
        }

        public bool IsSuccessStatusCode => StatusCode < HttpStatusCode.BadRequest;

        public string Message { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public T GetContent<T>() => (T)_content;
    }
}
