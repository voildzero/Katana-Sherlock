using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Katana_Sherlock
{
    public class UsernameChecker
    {
        private readonly List<string> foundLinks = new List<string>();
        private string username = string.Empty;
        private Stopwatch? stopwatch;

        public async Task RunAsync()
        {
            while (true)
            {
                Console.Write("Enter username: ");
#pragma warning disable CS8602
                username = Console.ReadLine().Trim();
#pragma warning restore CS8602
                Console.WriteLine("");

                if (string.IsNullOrWhiteSpace(username))
                {
                    Console.WriteLine("Username cannot be empty!");
                    continue;
                }

                var sites = new Dictionary<string, string>
                {
                    { "Telegram", $"https://t.me/{username}" },
                    { "GitHub", $"https://github.com/{username}" },
                    { "Instagram", $"https://www.instagram.com/{username}/" },
                    { "Twitter", $"https://twitter.com/{username}" },
                    { "Reddit", $"https://www.reddit.com/user/{username}" },
                    { "Youtube", $"https://www.youtube.com/@{username}" },
                    { "Facebook", $"https://www.facebook.com/{username}" },
                    { "LinkedIn", $"https://www.linkedin.com/in/{username}/" },
                    { "Pinterest", $"https://www.pinterest.com/{username}/" },
                    { "Tumblr", $"https://{username}.tumblr.com/" },
                    { "Snapchat", $"https://www.snapchat.com/add/{username}" },
                    { "Twitch", $"https://www.twitch.tv/{username}" },
                    { "Flickr", $"https://www.flickr.com/photos/{username}/" },
                    { "Vimeo", $"https://vimeo.com/user{username}" },
                    { "Spotify", $"https://open.spotify.com/user/{username}" },
                    { "TikTok", $"https://www.tiktok.com/@{username}" },
                    { "Quora", $"https://www.quora.com/profile/{username}" },
                    { "Blogger", $"https://{username}.blogspot.com/" },
                    { "VK", $"https://vk.ru/{username}" },
                    { "DeviantArt", $"https://www.deviantart.com/{username}" },
                    
                };

                using var httpClient = new HttpClient(new HttpClientHandler
                {
                    AllowAutoRedirect = false,
                })
                {
                    Timeout = TimeSpan.FromSeconds(10)
                };

                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64)");

                foundLinks.Clear();
                stopwatch = Stopwatch.StartNew(); 

                var tasks = new List<Task>();
                foreach (var site in sites)
                {
                    tasks.Add(CheckUsernameAsync(httpClient, site.Key, site.Value));
                }

                await Task.WhenAll(tasks).ConfigureAwait(false);
                stopwatch.Stop();

                Console.WriteLine("\nSearch completed!");
                Console.Beep();

                Console.Write("\nDo you want to generate a report? (y/n): ");
                var answer = Console.ReadLine()?.Trim().ToLower();

                if (answer == "y")
                {
                    GenerateAndOpenReport();
                    Thread.Sleep(3500);
                    DrawLogo logo = new DrawLogo();
                    logo.Clear();
                    logo.Draw();
                    Console.ResetColor();
                    Intro intro = new Intro();
                    intro.PrintIntro();
                }
                else if (answer == "n")
                {
                    DrawLogo logo = new DrawLogo();
                    Thread.Sleep(1000);
                    logo.Clear();
                    logo.Draw();
                    Console.ResetColor();
                    Intro intro = new Intro();
                    intro.PrintIntro();
                }
                else
                {
                    Console.WriteLine("Invalid input. Restarting search...");
                }
            }
        }

async Task CheckUsernameAsync(HttpClient client, string siteName, string url)
{
    try
    {
        var response = await client.GetAsync(url).ConfigureAwait(false);
        string responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

        bool exists = response.StatusCode == System.Net.HttpStatusCode.OK;

        if (exists)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{siteName}: [Found] | URL: {url}");
            foundLinks.Add($"{siteName}: {url}");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{siteName}: [Not found] | URL: {url}");
        }
    }
    catch (Exception ex)
    {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine($"{siteName}: [Error] | URL: {url} | Exception: {ex.Message}");
    }
    finally
    {
        Console.ResetColor();
    }
}
        private void GenerateAndOpenReport()
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            string htmlContent = $@"
<!DOCTYPE html>
<html lang='en'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>Katana-Sherlock Report</title>
    <style>
        body {{
            font-family: Arial, sans-serif;
            background-color: #0C0C0C;
            color: #ffffff;
            line-height: 1.6;
            margin: 20px;
        }}
        h1, h2, p {{
            margin-bottom: 15px;
        }}
        .found-links a {{
            color: #0ec414;
            text-decoration: none;
        }}
        .found-links a:hover {{
            text-decoration: underline;
        }}
    </style>
</head>
<body>
    <h1>KATANA-SHERLOCK SEARCH REPORT v1.2 | Developer: voild_official</h1>
    <h2>[ Start of report ]</h2>
    <p><strong>Search Target:</strong> {username}</p>
    <p><strong>Time Spent:</strong> {stopwatch.Elapsed.TotalSeconds:F2} seconds</p>
    <h3>Found At:</h3>
    <ul class='found-links'>
        {string.Join("\n", foundLinks.ConvertAll(link => $"<li><a href='{link.Split(": ")[1]}' target='_blank'>{link}</a></li>"))}
    </ul>
    
    <div class=""image-container"">
        <img src=""https://cdn.discordapp.com/attachments/1367161000469725214/1367162680627957781/IMG_20250430_181837_655.png?ex=681394f7&is=68124377&hm=e9041c76562f052b7bca98cd4acb7d1a2a69cd6fb6ce16f37d61bf6c62b11792&"" 
             alt=""Report Footer Image"" 
             class=""report-image""
             title=""Katana-Sherlock Logo"">
    </div>

    <h2>[ End of report ]</h2>
</body>
</html>";
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            string reportsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "reports");

            if (!Directory.Exists(reportsDirectory))
            {
                Directory.CreateDirectory(reportsDirectory);
            }
            
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            string randomCode = new string(Enumerable.Repeat(chars, 8)
                .Select(s => s[random.Next(s.Length)]).ToArray());
            
            string filePath = Path.Combine(reportsDirectory, $"Katana-Sherlock-Report-{username}_{randomCode}.html");
            File.WriteAllText(filePath, htmlContent);

            Console.WriteLine($"Report generated: {filePath}");

            Task.Delay(2000).Wait();
            Process.Start(new ProcessStartInfo
            {
                FileName = filePath,
                UseShellExecute = true
            });
        }
    }
}