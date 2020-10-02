using JiraClient.Api;

namespace JiraClient
{
  public class EndpointProvider : IEndpointProvider
  {
    public string GetAddAttachmentEndpoint(int issueID)
    {
      return GetAddAttachmentEndpoint(issueID.ToString());
    }

    public string GetAddAttachmentEndpoint(string issueID)
    {
      return $"rest/api/2/issue/{issueID}/attachments";
    }
  }
}
