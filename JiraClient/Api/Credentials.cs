namespace JiraClient.Api
{
  public readonly struct Credentials
  {
    public string Login { get; }
    public string Password { get; }

    public Credentials(string login, string password)
    {
      Login = login;
      Password = password;
    }
  }
}