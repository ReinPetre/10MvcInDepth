using System.Threading.Tasks;

namespace Beerhall.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
