using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;

namespace WriteToTopic
{
    class Program
    {
        const string ServiceBusConnectionString = "Endpoint=sb://neuron.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=mLQjdI6B4U83mG9EFLDhw9t1COULHHLkkyyghZ1/PJg=";
        const string TopicName = "policyevent";
        static ITopicClient topicClient;

        static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();

            Console.ReadKey();
        }

        static async Task MainAsync()
        {
            const int numberOfMessages = 10;
            topicClient = new TopicClient(ServiceBusConnectionString, TopicName);

            // Send messages.
            await SendMessagesAsync(numberOfMessages);
            await topicClient.CloseAsync();
        }

        static async Task SendMessagesAsync(int numberOfMessagesToSend)
        {
            try
            {
                // Create a new message to send to the topic
                string messageBody = @"{
                                        ""loanKey"": ""beb05058-8b0e-e811-80c7-0025b500b0db"",
                                        ""globalPropertyId"": 151042454,
                                        ""evaluationPeriod"": 
                                            {
                                            ""startDate"": ""2017-08-25"",
                                            ""endDate"": ""9999-12-31""
                                            }
                                        }";
                var message = new Message(Encoding.UTF8.GetBytes(messageBody));

                // Write the body of the message to the console
                Console.WriteLine($"Sending message: {messageBody}");

                // Send the message to the topic
                await topicClient.SendAsync(message);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"{DateTime.Now} :: Exception: {exception.Message}");
            }
        }
    }
}
