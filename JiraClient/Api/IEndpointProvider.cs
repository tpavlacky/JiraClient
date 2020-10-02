namespace JiraClient.Api
{
  public interface IEndpointProvider
  {
    string GetAddAttachmentEndpoint(int issueID);
    string GetAddAttachmentEndpoint(string issueID);
  }
}