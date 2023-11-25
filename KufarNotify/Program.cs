using KufarNotify;
using System.Net;
using System.Text.RegularExpressions;


TelegramManager tgManager = new TelegramManager();
Regex regEx = new Regex(@"<section><a class=.styles_wrapper__[0-9a-zA-Z]+.\s*href..(https...re.kufar.by.vi.minsk.snyat.kvartiru-dolgosrochno.[0-9]+).rank.[0-9]+.amp.searchId.[0-9a-z]+.");
RequestManager reqManager = new RequestManager();
string currentPosition = "";

while (true)
{
    var response = reqManager.GetResponseString();
    if (response == null)
    {
        tgManager.SendError404ToServiceAccount();
        await Task.Delay(10000);
        continue;
    }
    MatchCollection matches = regEx.Matches(response);
    if (matches.Count > 0)
    {
        if (matches.First().Groups[1].Value == currentPosition)
        {
            continue;
        }
        else
        {
            currentPosition = matches.First().Groups[1].Value;
            tgManager.SendNotidyAllClients(matches.First().Groups[1].Value);
        }
    }
    await Task.Delay(10000);
}