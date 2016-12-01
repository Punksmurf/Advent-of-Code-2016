using System;
using System.IO;
using System.Threading.Tasks;

namespace AoC2016.Service
{
    public class ChallengeDataFileService : IChallengeDataService
    {
        public bool CanSupplyData(int day)
        {
            return File.Exists(CreatePath(day));
        }

        public async Task<string> GetAsync(int day)
        {
            Console.WriteLine("Retrieving challenge data from file...");
            return await Task.Run(() => File.ReadAllText(CreatePath(day)));
        }

        public async Task SaveAsync(int day, string content)
        {
            var file = File.Create(CreatePath(day));
            using (var writer = new StreamWriter(file))
            {
                await writer.WriteAsync(content);
            }
        }

        private static string CreatePath(int day)
        {
            return $"Data/input-{day}.txt";
        }
    }
}