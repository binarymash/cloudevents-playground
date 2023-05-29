namespace BinaryMash.CloudEvents.Senders.Http;
using System.Net.Http;

public class HttpSender
{
    private readonly HttpClient _httpClient;
    
    public HttpSender(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task SendAsBinary(CloudEvent cloudEvent, CancellationToken cancellationToken)
    {
        var content = cloudEvent.ToHttpContent(ContentMode.Binary, new JsonEventFormatter());

        await _httpClient.PostAsync("some-endpoint", content, cancellationToken);
    }  

    public async Task SendAsStructured(CloudEvent cloudEvent, CancellationToken cancellationToken)
    {
        var content = cloudEvent.ToHttpContent(ContentMode.Structured, new JsonEventFormatter());

        await _httpClient.PostAsync("some-endpoint", content, cancellationToken);
    }

}
