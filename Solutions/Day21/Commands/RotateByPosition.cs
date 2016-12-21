using System;

namespace AoC2016.Solutions.Day21.Commands
{
    public class RotateByPosition : Command
    {
        public char Letter { get; }

        public RotateByPosition(char letter)
        {
            Letter = letter;
        }

        public override void Execute(ref char[] password)
        {
            RotateByPosition(ref password, Letter);
        }

        public override void ExecuteReverse(ref char[] password)
        {
            var endPosition = Array.IndexOf(password, Letter);

            var tempPassword = new char[password.Length];
            Array.Copy(password, tempPassword, password.Length);

            for (var i = 0; i < tempPassword.Length * 2; i++)
            {
                RotateLeft(ref tempPassword, 1);

                var index = Array.IndexOf(tempPassword, Letter);
                var amount = 1 + index;
                if (index >= 4) amount += 1;

                if ((index + amount) % tempPassword.Length == endPosition)
                {
                    RotateLeft(ref password, i+1);
                    return;
                }

            }
        }

        public override string ToString()
        {
            return $"RotateByPosition {Letter}";
        }

    }
}