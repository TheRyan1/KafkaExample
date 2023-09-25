using Confluent.Kafka;

namespace ConsoleConsumer
{
    public class KafkaConsumer
    {
        private readonly IConsumer<Ignore, string> consumer;
        private readonly CancellationTokenSource cancellationTokenSource;

        public KafkaConsumer(ConsumerConfig config)
        {
            
            consumer = new ConsumerBuilder<Ignore, string>(config).Build();

            // Replace with your topic name
            consumer.Subscribe("TestTopic"); 

            cancellationTokenSource = new CancellationTokenSource();

            // Start consuming messages
            Task.Run(() => ConsumeMessagesAsync(), cancellationTokenSource.Token);
        }

        private async Task ConsumeMessagesAsync()
        {
            while (!cancellationTokenSource.Token.IsCancellationRequested)
            {
                try
                {
                    var consumeResult = consumer.Consume(cancellationTokenSource.Token);

                    Console.WriteLine($"Message consumed : {consumeResult.Message.Value}");


                    consumer.Commit(consumeResult);
                    Console.WriteLine($"Message commited as read");

                }
                catch (OperationCanceledException)
                {
                    // Handle the cancellation request gracefully
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            consumer.Close();
        }

        public void CancelConsuming()
        {
            cancellationTokenSource.Cancel();
        }
    }
}
