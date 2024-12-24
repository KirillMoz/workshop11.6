using TelegramBot.Models;

namespace TelegramBot.Services
{
    public interface IStorage
    {
        Session GetSession(long chatId);
    }
}
