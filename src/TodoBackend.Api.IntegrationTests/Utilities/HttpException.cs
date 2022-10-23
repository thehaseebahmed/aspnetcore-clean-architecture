using System.Net;

namespace Starter.Api.IntegrationTests.Utilities;

public class HttpException : Exception
{
    public HttpStatusCode StatusCode { get; }
    public string Content { get; }

    public HttpException(string message, HttpStatusCode statusCode, string content) : base(message)
    {
        StatusCode = statusCode;
        Content = content;
    }
}