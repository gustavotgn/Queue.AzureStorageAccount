using System.Threading.Tasks;

namespace Queue.Repository
{
    public interface IQueueRepository
    {
        Task SendMessage(string queueName, string message);
        Task<string> ReceiveMessage(string queueName);
    }
}