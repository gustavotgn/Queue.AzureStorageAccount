using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using System;
using System.Threading.Tasks;

namespace Queue.Repository
{
    public class QueueRepository : IQueueRepository
    {
        IConfiguration _configuration;
        public QueueRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public async Task<string> ReceiveMessage(string queueName)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(this.GetConnection());
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
            CloudQueue queue = queueClient.GetQueueReference(queueName);

            bool exists = await queue.ExistsAsync();

            if (exists)
            {
                CloudQueueMessage retrievedMessage = await queue.GetMessageAsync();

                if (retrievedMessage != null)
                {
                    string theMessage = retrievedMessage.AsString;
                    await queue.DeleteMessageAsync(retrievedMessage);
                    return theMessage;
                }
                else
                {
                    Console.Write("The queue is empty. Attempt to delete it? (Y/N) ");
                    string response = Console.ReadLine();

                    if (response == "Y" || response == "y")
                    {
                        await queue.DeleteIfExistsAsync();
                        return "The queue was deleted.";
                    }
                    else
                    {
                        return "The queue was not deleted.";
                    }
                }
            }
            else
            {
                return "The queue does not exist. Add a message to the command line to create the queue and store the message.";
            }
        }

        public async Task SendMessage(string queueName, string message)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(this.GetConnection());
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
            CloudQueue queue = queueClient.GetQueueReference(queueName);

            bool createdQueue = await queue.CreateIfNotExistsAsync();

            if (createdQueue)
            {
                Console.WriteLine("The queue was created.");
            }

            CloudQueueMessage queuemessage = new CloudQueueMessage(message);
            await queue.AddMessageAsync(queuemessage);
        }

        private string GetConnection()
        {
            var connection = _configuration.GetSection("AzureWebJobsStorage").Value;
            return connection;
        }
    }
}
