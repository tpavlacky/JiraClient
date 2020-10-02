using System;
using System.Threading.Tasks;
using Flurl.Http;
using JiraClient.Api;

namespace JiraClient.Flurl
{
  internal class FlurlClient : IJiraClient
  {
    private readonly string _authToken;
    private readonly Uri _serverUri;
    private readonly IEndpointProvider _endpointProvider;

    public FlurlClient(Credentials credentials, Uri serverUri, IEndpointProvider endpointProvider)
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
      var endpointAddress = _serverUri + _endpointProvider.GetAddAttachmentEndpoint(issueID);
      var response = await 
        CreateRequest(endpointAddress)
        //.WithHeader("Content-Type", "multipart/form-data")
        .PostMultipartAsync(mp => mp.AddFile("file", attachmentPath));

      return new Response(response.ResponseMessage.StatusCode, await response.ResponseMessage.Content.ReadAsStringAsync());
    }

    private IFlurlRequest CreateRequest(string endpointAddress)
    {
      return endpointAddress
        .WithHeader("X-Atlassian-Token", "no-check")
        .WithHeader("Authorization", $"Basic {_authToken}");
    }
  }
}
