using System.Text;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace SportsResults;

public class SportsScraper {
    public static string Scrape()
    {
        HtmlWeb web = new HtmlWeb();
        var doc = web.Load("https://www.basketball-reference.com/boxscores/");

        var gameSummaries = doc.DocumentNode.SelectNodes("//*[@id=\"content\"]/div[3]/div");

        StringBuilder emailBody = new StringBuilder();
        emailBody.AppendLine($"<html><body><h1>Games from today (ish)</h1>");

        foreach (var summary in gameSummaries)
        {
            var tables = summary.SelectNodes(".//table");

            if (tables != null)
            {
                foreach(var table in tables)
                {
                    string tableHtml = table.OuterHtml.Replace("&nbsp;", " ");
                    string html = Regex.Replace(tableHtml, @"href\s*=\s*""[^""]*""", string.Empty);
                    emailBody.Append(html);
                    emailBody.Append("<br>"); 
                }
            }
            emailBody.Append("<p>----------------------------------</p>");
        }

        emailBody.Append("</body></html>");

        return emailBody.ToString();
    }
}