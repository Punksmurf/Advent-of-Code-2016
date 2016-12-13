using System.Collections.Generic;

namespace AoC2016.Solutions.Day11.State
{
    public interface IBuilding
    {
        bool IsFinished();
        void Print();
        IEnumerable<ICommand> CreatePossibleCommands();
        IBuilding ExecuteCommand(ICommand command);
    }
}