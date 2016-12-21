namespace AoC2016.Solutions.Day21.Commands
{
    public class RotateLeft : Command
    {
        public int Amount { get; }

        public RotateLeft(int amount)
        {
            Amount = amount;
        }

        public override void Execute(ref char[] password)
        {
            RotateLeft(ref password, Amount);
        }

        public override void ExecuteReverse(ref char[] password)
        {
            RotateRight(ref password, Amount);
        }

        public override string ToString()
        {
            return $"RotateLeft {Amount}";
        }

    }
}