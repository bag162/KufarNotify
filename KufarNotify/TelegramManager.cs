using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace KufarNotify
{
    public class TelegramManager
    {
        static List<Update> userList = new List<Update>();
        static ITelegramBotClient bot = new TelegramBotClient("6835347984:AAGqtGDLtt_iN4p9G64Pxn0p6jL9V2i_KcE");

        public TelegramManager()
        {
            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { }, // receive all update types
            };
            bot.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken
            );
        }

        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update)); // TODO delete this
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                var message = update.Message;
                if (message.Text.ToLower() == "/start")
                {
                    if(userList.Contains(update))
                    {
                        await botClient.SendTextMessageAsync(message.Chat, "Ты уже помечен как черт братан, но пока что только карандашом.");
                    }
                    else
                    {
                        userList.Add(update);
                        await botClient.SendTextMessageAsync(message.Chat, "Добро пожаловать на борт, добрый путник! Вы подписаны на мои уведомления, и теперь будете получать их всегда, пока и не удалю сервер, или пока вы не удалите меня :(.");
                    }
                    return;
                }
                await botClient.SendTextMessageAsync(message.Chat, "Не пиши мне ничего. Я все равно никак не обрабатываю твои данные, засоряешь только линию.");
            }
        }

        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            foreach (Update user in userList)
            {
                bot.SendTextMessageAsync(user.Message.Chat, "Братик, тут эксепшен ко мне приехал. Покажи его разрабу.");
                bot.SendTextMessageAsync(user.Message.Chat, Newtonsoft.Json.JsonConvert.SerializeObject(exception));
            }
        }

        public void SendNotidyAllClients(string linq)
        {
            foreach(Update user in userList)
            {
                bot.SendTextMessageAsync(user.Message.Chat, "Братуля, тут новая хатка. Хватай пока еще не разбрали");
                bot.SendTextMessageAsync(user.Message.Chat, linq);
            }
        }

        public void SendError404AllClients()
        {
            foreach (Update user in userList)
            {
                bot.SendTextMessageAsync(user.Message.Chat, "Братуля. У нас 100 ошибок 404 накопилось.");
            }
        }
    }
}
