namespace AoC2016.Solutions.Day21.Commands
{
    public class SwapLetters : Command
    {
        public char A { get; }
        public char B { get; }

        public SwapLetters(char a, char b)
        {
            A = a;
            B = b;
        }

        public override void Execute(ref char[] password)
        {
            SwapLetters(ref password, A, B);
        }

        public override void ExecuteReverse(ref char[] password)
        {
            SwapLetters(ref password, A, B);
        }


        public override string ToString()
        {
            return $"SwapLetters {A} {B}";
        }
    }
}