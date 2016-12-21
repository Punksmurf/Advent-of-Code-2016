namespace AoC2016.Solutions.Day21.Commands
{
    public class Move : Command
    {
        public int A { get; }
        public int B { get; }

        public Move(int a, int b)
        {
            A = a;
            B = b;
        }

        public override void Execute(ref char[] password)
        {
            Move(ref password, A, B);
        }

        public override void ExecuteReverse(ref char[] password)
        {
            Move(ref password, B, A);
        }

        public override string ToString()
        {
            return $"Move {A} {B}";
        }
    }
}