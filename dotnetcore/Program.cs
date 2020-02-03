using System;
using System.Text;
using System.Threading;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;

namespace dotnetcore
{
    class Program
    {
        private static readonly AutoResetEvent _closing = new AutoResetEvent(false);

        static void Main(string[] args)
        {
            var hostname = "localhost";
            if (args.Length > 0)
            {
                hostname = args[0];
            }

            Console.WriteLine("Starting...");

            var factory = new MqttFactory();
            var client = factory.CreateMqttClient();

            var options = new MqttClientOptionsBuilder()
                .WithClientId(Guid.NewGuid().ToString())
                .WithTcpServer(hostname)
                .Build();

            client.UseConnectedHandler(async e =>
            {
                Console.WriteLine($"Connect to {hostname} on port 1883 ...");
                await client.SubscribeAsync(new TopicFilterBuilder().WithTopic("temperature/+").Build());
            });

            client.UseApplicationMessageReceivedHandler(e =>
            {
                var value = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                Console.WriteLine($"Received: {e.ApplicationMessage.Topic}: {value}");
            });

            Console.WriteLine($"Try to connect to {hostname} on port 1883 ...");

            client.ConnectAsync(options, CancellationToken.None);

            Console.CancelKeyPress += new ConsoleCancelEventHandler(OnExit);
            _closing.WaitOne();


        }
        protected static void OnExit(object sender, ConsoleCancelEventArgs args)
        {
            Console.WriteLine("Exit");
            _closing.Set();
        }
    }
}
