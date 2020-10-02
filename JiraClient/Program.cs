using System;
using System.Threading.Tasks;
using JiraClient.Api;
using JiraClient.Flurl;
using JiraClient.RestSharp;

namespace JiraClient
{
  internal class Program
  {
    private static async Task Main()
    {
      var credentials = new Credentials("admin", "admin");
      var serverUri = new Uri("http://192.168.99.104:8080");
      var endpointProvider = new EndpointProvider();

      var client = CreateClient(ClientType.Flurl, credentials, serverUri, endpointProvider);
      var response = await client.AddAttachment(10000, @"C:\Users\Z003Z8FE\Desktop\gemalto.PNG");

      Console.WriteLine("Status code: " + response.Code);
      Console.WriteLine(response.Message);
      Console.ReadLine();
    }

    private static IJiraClient CreateClient(ClientType clientType, Credentials credentials, Uri serverUri, IEndpointProvider endpointProvider)
    {
      return clientType switch
      {
        ClientType.RestSharp => new RestSharpClient(credentials, serverUri, endpointProvider),
        ClientType.Flurl => new FlurlClient(credentials, serverUri, endpointProvider),
        _ => throw new ArgumentOutOfRangeException(nameof(clientType), clientType, null)
      };
    }

    private enum ClientType
    {
      RestSharp,
      Flurl
    }
  }
}
