using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _55rCheckerWF
{
    public static class TextHelper
    {
        public static CheckMessage GetMessage(string html, out double percent)
        {
            CheckMessage message = new CheckMessage();
            Match match = Regex.Match(html, @"<p>用户：<code>(.+)</code>");
            message.Username = match.Groups[1].Value.Trim();
            match = Regex.Match(html, @"等级：.+<code>(.+)</code>");
            message.Level = match.Groups[1].Value.Trim();
            match = Regex.Match(html, @"总流量：<code>(.+)</code>");
            message.All = match.Groups[1].Value.Trim();
            match = Regex.Match(html, @"已用流量：<code>(.+)</code>");
            message.Used = match.Groups[1].Value.Trim();
            match = Regex.Match(html, @"剩余流量：<code>(.+)</code>");
            message.Surplus = match.Groups[1].Value.Trim();
            double all = 1;
            double.TryParse(Regex.Match(message.All, @"[0-9]+").Value, out all);
            double used = 0;
            double.TryParse(Regex.Match(message.Used, @"[0-9]+").Value, out used);
            if (message.All.EndsWith("MB"))
            {
                all = all * 1024 * 1024;
            }
            else if (message.All.EndsWith("GB"))
            {
                all = all * 1024 * 1024 * 1024;
            }
            else if (message.All.EndsWith("TB"))
            {
                all = all * 1024 * 1024 * 1024 * 1024;
            }
            if (message.Used.EndsWith("MB"))
            {
                used = used * 1024 * 1024;
            }
            else if (message.Used.EndsWith("GB"))
            {
                used = used * 1024 * 1024;
            }
            else if (message.Used.EndsWith("TB"))
            {
                used = used * 1024 * 1024 * 1024 * 1024;
            }
            percent = used / all;
            return message;
        }
    }
}
