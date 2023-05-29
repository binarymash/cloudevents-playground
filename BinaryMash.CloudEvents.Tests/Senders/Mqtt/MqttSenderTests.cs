namespace BinaryMash.CloudEvents.Tests.Senders.Mqtt;

using BinaryMash.CloudEvents.Senders.Mqtt;
using FluentAssertions;
using Moq;
using MQTTnet;

public class MqttSenderTests
{
    Mock<MQTTnet.IApplicationMessagePublisher> _mqttPublisher;
    
    MqttSender _sut;

    MqttApplicationMessage? _publishedMessage;

    public MqttSenderTests()
    {
        _mqttPublisher = new Mock<MQTTnet.IApplicationMessagePublisher>();
        
        _mqttPublisher
            .Setup(p => p.PublishAsync(It.IsAny<MqttApplicationMessage>(), It.IsAny<CancellationToken>()))
            .Callback<MqttApplicationMessage, CancellationToken>((m, ct) => {_publishedMessage = m;})
            .ReturnsAsync(new MQTTnet.Client.Publishing.MqttClientPublishResult());

        _sut = new MqttSender(_mqttPublisher.Object);   
    }

    [Xunit.Fact]
    public async Task SendAsStructured()
    {
        // Given
        CloudEvent projectCreatedEvent = new CloudEventBuilder().BuildMinimal();

        // When
        await _sut.SendAsStructured(projectCreatedEvent, default);

        // Then
        Snapshot.Match(_publishedMessage, "Structured");

        var payload = System.Text.Encoding.UTF8.GetString(_publishedMessage!.Payload);
        Snapshot.Match(payload, "Structured.Payload");
    }   

    [Fact]
    public async Task SendAsBinary()
    {
        // Given
        CloudEvent projectCreatedEvent = new CloudEventBuilder().BuildMinimal();

        // When
        var thrownException = await Assert.ThrowsAsync<ArgumentOutOfRangeException>( () => _sut.SendAsBinary(projectCreatedEvent, default));

        // Then
        thrownException.Message.Should().Be("Unsupported content mode: Binary (Parameter 'contentMode')");
    } 
}