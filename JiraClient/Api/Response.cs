using System.Net;

namespace JiraClient.Api
{
  public class Response : IResponse
  {
    public HttpStatusCode Code { get; }
    public string Message { get; }

    public Response(HttpStatusCode code, string message)
    {
      Code = code;
      Message = message;
    }
  }
}