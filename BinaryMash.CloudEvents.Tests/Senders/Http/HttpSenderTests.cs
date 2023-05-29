namespace BinaryMash.CloudEvents.Tests.Senders.Http;

using BinaryMash.CloudEvents.Senders.Http;

public class MqttSenderTests
{
    HttpContent? _sentRequest = null!;
    HttpSender _sut = null!;

    public MqttSenderTests()
    {
         _sut = SetUpSubject();        
    }

    [Fact]
    public async Task SendAsStructuredHttp()
    {
        // Given
        CloudEvent projectCreatedEvent = new CloudEventBuilder().BuildMinimal();

        // When
        await _sut.SendAsStructured(projectCreatedEvent, default);

        // Then
        Snapshot.Match(_sentRequest?.Headers, "Structured.Headers");
        Snapshot.Match(_sentRequest?.ReadAsStringAsync().Result, "Structured.Body");
    }   

    [Fact]
    public async Task SendAsBinaryHttp()
    {
        // Given
        CloudEvent projectCreatedEvent = new CloudEventBuilder().BuildMinimal();

        // When
        await _sut.SendAsBinary(projectCreatedEvent, default);

        // Then
        Snapshot.Match(_sentRequest?.Headers, "Binary.Headers");
        Snapshot.Match(_sentRequest?.ReadAsStringAsync().Result, "Binary.Body");
    } 

    [Fact]
    public async Task SendWithPartitionAsBinaryHttp()
    {
        // Given
        CloudEvent projectCreatedEvent = new CloudEventBuilder().BuildMinimalWithPartitionExtension();

        // When
        await _sut.SendAsBinary(projectCreatedEvent, default);

        // Then
        Snapshot.Match(_sentRequest?.Headers, "Binary.Partition.Headers");
        Snapshot.Match(_sentRequest?.ReadAsStringAsync().Result, "Binary.Partition.Body");
    }     

    private HttpSender SetUpSubject()
    {
        TestHttpMessageHandler messageHandler = new((request, ct) =>
        {
            _sentRequest = request?.Content;
            return Task.FromResult(new HttpResponseMessage());
        });

        HttpClient httpClient = new(messageHandler)
        {
            BaseAddress = new Uri("https://some.url.com")
        };

        HttpSender sut = new(httpClient);
        return sut;
    }
}