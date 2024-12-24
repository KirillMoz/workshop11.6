using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.Enums;
using System.Collections.Generic;
using TelegramBot.Services;
using TelegramBot.Models;
using TelegramBot.Utilities;

namespace TelegramBot.Controllers
{
    public class TextMessageController
    {
        private readonly ITelegramBotClient _telegramClient;
        private readonly IStorage _memoryStorage;

        public TextMessageController(ITelegramBotClient telegramBotClient, IStorage memoryStorage)
        {
            _telegramClient = telegramBotClient;
            _memoryStorage = memoryStorage;
        }

        [Obsolete]
        public async Task Handle(Message message, CancellationToken ct)
        {
            switch (message.Text)
            {
                case "/start":
                    var screenButtons = new List<InlineKeyboardButton[]>();
                    screenButtons.Add(new[] {InlineKeyboardButton.WithCallbackData($"Посчитать количество символов" , $"GetCountSymbols"),
                                            InlineKeyboardButton.WithCallbackData($"Сложить числа" , $"Summary") });
                    
                    await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"<b>  Тестовые функции ТГ бота на C#</b> {Environment.NewLine}", 
                        cancellationToken: ct, parseMode: ParseMode.Html, replyMarkup: new InlineKeyboardMarkup(screenButtons));
                    break;
                default:
                    string functionCode = _memoryStorage.GetSession(message.Chat.Id).FuctionCode;
                    switch (functionCode)
                    {
                        case "GetCountSymbols":
                            await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"Количество символов = {message.Text.Length}", cancellationToken: ct);
                            break;
                        case "Summary":
                            int Summ = HelpString.GetSummary(message.Text, ' ');
                            await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"Сумма чисел = {Summ}", cancellationToken: ct);
                            break;
                        default:
                            await _telegramClient.SendTextMessageAsync(message.Chat.Id, "Ошибка!!! Ввели неизвестную команду!!!", cancellationToken: ct);
                            break;
                     
                    }
                    break;
            }
        }
    }
}
