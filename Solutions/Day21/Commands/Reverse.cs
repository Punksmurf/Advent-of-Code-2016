namespace AoC2016.Solutions.Day21.Commands
{
    public class Reverse : Command
    {
        public int A { get; }
        public int B { get; }

        public Reverse(int a, int b)
        {
            A = a;
            B = b;
        }

        public override void Execute(ref char[] password)
        {
            Reverse(ref password, A, B);
        }

        public override void ExecuteReverse(ref char[] password)
        {
            Reverse(ref password, A, B);
        }

        public override string ToString()
        {
            return $"Reverse {A} {B}";
        }

    }
}