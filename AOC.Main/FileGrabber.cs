using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace AOC
{
    public class FileGrabber
    {
        // I give it 3 days before I push my sessioncookie
        const string SessionCookie = "SECRETS ARE ANNOYING CBF";
        public static async Task<String> LoadFile(int day)
        {
            var fileName = $"Day{day}.txt";

            if (!File.Exists(fileName))
            {
                try
                {
                    var file = await DownloadFile(day);
                    await File.WriteAllTextAsync(fileName, file);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return await File.ReadAllTextAsync(fileName);
        }
        private async static Task<string> DownloadFile(int day)
        {
            if (day < 1 || day > 25)
                throw new ApplicationException("Invalid Day");

            HttpClientHandler handler = new HttpClientHandler
            {
                CookieContainer = new CookieContainer(),
            };
            handler.CookieContainer.Add(new Uri("https://adventofcode.com"), new Cookie("session", SessionCookie));

            using (HttpClient client = new HttpClient(handler))
            {
                HttpResponseMessage response = await client.GetAsync($"https://adventofcode.com/2021/day/{day}/input");

                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadAsStringAsync();
                else
                    throw new ApplicationException(response.ReasonPhrase);
            }
        }
    }
}
