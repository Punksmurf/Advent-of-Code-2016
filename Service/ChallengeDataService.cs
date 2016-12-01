using System.Threading.Tasks;

namespace AoC2016.Service
{
    public class ChallengeDataService : IChallengeDataService
    {
        private readonly ChallengeDataFileService _fileService;
        private readonly ChallengeDataWebService _webService;

        public ChallengeDataService(string sessionToken)
        {
            _fileService = new ChallengeDataFileService();
            _webService = new ChallengeDataWebService(sessionToken);
        }

        public async Task<string> GetAsync(int day)
        {
            if (_fileService.CanSupplyData(day))
            {
                return await _fileService.GetAsync(day);
            }

            var content = await _webService.GetAsync(day);
            await _fileService.SaveAsync(day, content);
            return content;
        }
    }
}