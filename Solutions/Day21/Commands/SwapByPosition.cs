namespace AoC2016.Solutions.Day21.Commands
{
    public class SwapByPosition : Command
    {
        public int A { get; }
        public int B { get; }

        public SwapByPosition(int a, int b)
        {
            A = a;
            B = b;
        }

        public override void Execute(ref char[] password)
        {
            Swap(ref password, A, B);
        }

        public override void ExecuteReverse(ref char[] password)
        {
            Swap(ref password, A, B);
        }

        public override string ToString()
        {
            return $"SwapByPosition {A} {B}";
        }
    }
}