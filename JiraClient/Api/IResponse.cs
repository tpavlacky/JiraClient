using System.Net;

namespace JiraClient.Api
{
  public interface IResponse
  {
    HttpStatusCode Code { get; }
    string Message { get; }
  }
}