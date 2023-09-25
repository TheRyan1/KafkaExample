// See https://aka.ms/new-console-template for more information


using Confluent.Kafka;
using ConsoleConsumer;

var config = new ConsumerConfig
{
    BootstrapServers = "localhost:29092",
    GroupId = "my-consumer-group", 
    AutoOffsetReset = AutoOffsetReset.Earliest
};

var consumer = new KafkaConsumer(config);

Console.WriteLine("Press [Enter] to stop the consumer.");
Console.ReadLine();

consumer.CancelConsuming();





