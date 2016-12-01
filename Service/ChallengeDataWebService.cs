using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace AoC2016.Service
{
    public class ChallengeDataWebService : IChallengeDataService
    {
        private readonly CookieContainer _cookies = new CookieContainer();
        private readonly HttpClientHandler _handler = new HttpClientHandler();

        public ChallengeDataWebService(string sessionToken)
        {
            _handler.CookieContainer = _cookies;
            _handler.UseCookies = true;

            var sessionCookie = new Cookie("session", sessionToken)
            {
                Domain = "adventofcode.com",
                Path = "/"
            };
            _cookies.Add(new Uri("http://adventofcode.com"), sessionCookie);

        }

        public async Task<string> GetAsync(int day)
        {
            Console.WriteLine("Retrieving challenge data from web...");
            using (var client = new HttpClient(_handler))
            {
                var response = await client.GetAsync($"http://adventofcode.com/2016/day/{day}/input");
                if (!response.IsSuccessStatusCode)
                {
                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.NotFound:
                            Console.WriteLine("The challenge data could not be found. Did you request a valid day?");
                            break;
                        default:
                            Console.WriteLine("The request could not be completed successfully. Did you supply a valid session token?");
                            break;
                    }
                    throw new IOException(response.ReasonPhrase);
                }
                var content = await response.Content.ReadAsStringAsync();
                return content;
            }
        }
    }
}