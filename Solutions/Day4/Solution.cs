using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AoC2016.Solutions.Day4
{
    public class Solution : ISolution
    {
        public Task<string> SolveExampleAsync()
        {
            const string input = @"aaaaa-bbb-z-y-x-123[abxyz]
a-b-c-d-e-f-g-h-987[abcde]
not-a-real-room-404[oarel]
totally-real-room-200[decoy]";

            return Task.FromResult(SolveSilver(input));
        }

        public async Task<string> SolveSilverAsync(string input)
        {
            return await Task.Run(() => SolveSilver(input));
        }

        public async Task<string> SolveGoldAsync(string input)
        {
            return await Task.Run(() => SolveGold(input));
        }

        private static string SolveSilver(string input)
        {
            // ToList to prevent re-enumeration to provide the count values in the result
            var allRooms = Room.Factory.CreateIEnumerable(input).ToList();
            var validRooms = allRooms.Where(_ => _.IsValid()).ToList();
            var sectorSum = validRooms.Sum(_ => _.Sector);

            DumpAllRooms(allRooms);

            return $"There are {allRooms.Count} rooms on the list, but only {validRooms.Count} are valid. The sum of the sector ids is {sectorSum}";
        }

        private static string SolveGold(string input)
        {
            var secretRoom = Room.Factory.CreateIEnumerable(input).AsParallel()
                .Where(_ => _.IsValid())
                .First(_ => _.GetDecryptedName().Equals("northpole object storage"));


            return $"The sector id from the secret room is {secretRoom.Sector}";
        }

        private static void DumpAllRooms(List<Room> rooms)
        {
            Console.WriteLine("All rooms:");
            rooms.ForEach(_ => Console.WriteLine(_.ToString()));
        }
    }
}