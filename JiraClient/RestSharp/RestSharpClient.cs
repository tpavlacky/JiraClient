using System;
using System.Threading.Tasks;
using JiraClient.Api;
using RestSharp;

namespace JiraClient.RestSharp
{
  public class RestSharpClient : IJiraClient
  {
    private readonly string _authToken;
    private readonly Uri _serverUri;
    private readonly IEndpointProvider _endpointProvider;

    public RestSharpClient(Credentials credentials, Uri serverUri, IEndpointProvider endpointProvider)
    {
      if (string.IsNullOrEmpty(credentials.Password) || string.IsNullOrEmpty(credentials.Login))
      {
        throw new Exception("Login and password need to be supplied");
      }

      _authToken = Base64Converter.Encode($"{credentials.Login}:{credentials.Password}");
      _serverUri = serverUri ?? throw new ArgumentNullException(nameof(serverUri));
      _endpointProvider = endpointProvider ?? throw new ArgumentNullException(nameof(endpointProvider));
    }

    public async Task<IResponse> AddAttachment(int issueID, string attachmentPath)
    {
      return await AddAttachment(issueID.ToString(), attachmentPath);
    }

    public async Task<IResponse> AddAttachment(string issueID, string attachmentPath)
    {
      var client = CreateClient(_endpointProvider.GetAddAttachmentEndpoint(issueID));

      var request = CreateRequest(Method.POST);
      request.AddHeader("Content-Type", "multipart/form-data");
      request.AddFile("file", attachmentPath);

      var response = await client.ExecutePostTaskAsync(request);
      return new Response(response.StatusCode, response.Content);
    }

    private RestRequest CreateRequest(Method method)
    {
      var request = new RestRequest(method);
      request.AddHeader("X-Atlassian-Token", "no-check");
      request.AddHeader("Authorization", $"Basic {_authToken}");

      return request;
    }

    private RestClient CreateClient(string endpoint)
    {
      return new RestClient(_serverUri + endpoint)
      {
        Timeout = -1
      };
    }
  }
}