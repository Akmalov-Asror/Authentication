namespace Teaching.Common.IntergationServices;

public class GithubServices
{
    private const int MaxRetries = 4;
    private readonly IHttpClientFactory _httpClientFactory;
    private static readonly Random Random = new Random();

    public GithubServices(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
}
