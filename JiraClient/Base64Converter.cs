using System;

namespace JiraClient
{
  public static class Base64Converter
  {
    public static string Encode(string plainText)
    {
      var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
      return Convert.ToBase64String(plainTextBytes);
    }
  }
}
