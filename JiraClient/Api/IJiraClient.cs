using System.Threading.Tasks;

namespace JiraClient.Api
{
  public interface IJiraClient
  {
    Task<IResponse> AddAttachment(int issueID, string attachmentPath);
    Task<IResponse> AddAttachment(string issueID, string attachmentPath);
  }
}