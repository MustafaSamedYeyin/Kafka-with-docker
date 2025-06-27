using System;
using System.Threading.Tasks;
using Confluent.Kafka;
using core.Interfaces;

namespace service.services;

public class Kafka : IKafka
{
    public string Topic { get; set; } = "MySampleTopic";

    public async Task<bool> ProduceMessage(string message)
    {
        var producerConfig = GetProducerConfig();

        var producer = GetProducer(producerConfig);

        await ProducerMessage(message, producer);
        return true;
    }

    #region Producer
    private IProducer<string, string> GetProducer(ProducerConfig producerConfig)
    {
        return new ProducerBuilder<string, string>(producerConfig).Build();
    }

    private async Task ProducerMessage(string message, IProducer<string, string> producer)
    {
        await producer.ProduceAsync(Topic, new Message<string, string>
        {
            Key = GetKey(),
            Value = message
        });
    }

    private ProducerConfig GetProducerConfig()
    {
        return new ProducerConfig()
        {
            BootstrapServers = "kafka:9092"
        };
    }

    private string GetKey()
    {
        return Guid.NewGuid().ToString();
    }

    #endregion

    public string ConsumeMessages(int bookmark,int offset)
    {
        var consumerConfig = GetConsumerConfig();

        var consumer = GetConsumer(consumerConfig);
        consumer.Subscribe(Topic);

        return GetConsumerMessage(consumer);
    }

    #region Consumer

    private string  GetConsumerMessage(IConsumer<string, string> consumer)
    {
        var consumerResult = consumer.Consume(TimeSpan.FromSeconds(10));
        return consumerResult.Message.Value;
    }

    private IConsumer<string, string> GetConsumer(ConsumerConfig consumerConfig)
    {
        return new ConsumerBuilder<string, string>(consumerConfig).Build();
    }

    private ConsumerConfig GetConsumerConfig()
    {
        return new ConsumerConfig()
        {
            BootstrapServers = "kafka:9092",
            GroupId = "MySampleGroup",
            AutoOffsetReset = AutoOffsetReset.Earliest, // Changed to Earliest to read existing messages
            EnableAutoCommit = false
        };
    }
    #endregion

}