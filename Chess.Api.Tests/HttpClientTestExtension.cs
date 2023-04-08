namespace Chess.Api.Tests;

public static class HttpClientTestExtension
{
    public static HttpClient Next(this HttpClient client)
    {
        if (client == Context.UserOneClient)
            return Context.UserTwoClient;
        return Context.UserOneClient;
    }
}