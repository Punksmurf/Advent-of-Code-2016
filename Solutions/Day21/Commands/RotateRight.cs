namespace AoC2016.Solutions.Day21.Commands
{
    public class RotateRight : Command
    {
        public int Amount { get; }

        public RotateRight(int amount)
        {
            Amount = amount;
        }

        public override void Execute(ref char[] password)
        {
            RotateRight(ref password, Amount);
        }

        public override void ExecuteReverse(ref char[] password)
        {
            RotateLeft(ref password, Amount);
        }

        public override string ToString()
        {
            return $"RotateLeft {Amount}";
        }
    }
}