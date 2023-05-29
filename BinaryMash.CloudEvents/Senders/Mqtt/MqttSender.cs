using CloudNative.CloudEvents.Mqtt;
using MQTTnet;

namespace BinaryMash.CloudEvents.Senders.Mqtt;

public class MqttSender
{   
    IApplicationMessagePublisher _mqttPublisher;

    public MqttSender(IApplicationMessagePublisher mqttPublisher)
    {
        _mqttPublisher = mqttPublisher;
    }

    public async Task SendAsBinary(CloudEvent cloudEvent, CancellationToken cancellationToken)
    {
        var content = cloudEvent.ToMqttApplicationMessage(ContentMode.Binary, new JsonEventFormatter(), "my_mqtt_topic");
        content.ResponseTopic = "my_response_topic";
        content.QualityOfServiceLevel = MQTTnet.Protocol.MqttQualityOfServiceLevel.ExactlyOnce;

        var result = await _mqttPublisher.PublishAsync(content, cancellationToken);
    }

    public async Task SendAsStructured(CloudEvent cloudEvent, CancellationToken cancellationToken)
    {
        var content = cloudEvent.ToMqttApplicationMessage(ContentMode.Structured, new JsonEventFormatter(), "my_mqtt_topic");
        content.ResponseTopic = "my_response_topic";
        content.QualityOfServiceLevel = MQTTnet.Protocol.MqttQualityOfServiceLevel.ExactlyOnce;

        var result = await _mqttPublisher.PublishAsync(content, cancellationToken);
    } 
}
