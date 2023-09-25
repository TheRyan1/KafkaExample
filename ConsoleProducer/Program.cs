// See https://aka.ms/new-console-template for more information

using Confluent.Kafka;

var config = new ProducerConfig
{
    // Replace with your Kafka broker address
    BootstrapServers = "localhost:9092", 
};

while (true)
{
    string userMessage = "";
    Console.WriteLine("Enter Message to produce : ");
    userMessage = Console.ReadLine();


    using (var producer = new ProducerBuilder<Null, string>(config).Build())
    {
        var topicName = "TestTopic";

        var message = new Message<Null, string>
        {
            Value = userMessage
        };

        try
        {
            var deliveryResult = await producer.ProduceAsync(topicName, message);
            Console.WriteLine($"Message delivered to: {deliveryResult.Topic}, Partition: {deliveryResult.Partition}, Offset: {deliveryResult.Offset}");
        }
        catch (ProduceException<Null, string> ex)
        {
            Console.WriteLine($"Error: {ex.Error.Reason}");
        }
    }
}
