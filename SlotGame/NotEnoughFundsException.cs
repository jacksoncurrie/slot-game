using System;

namespace SlotGame
{
    class NotEnoughFundsException : Exception
    {
        public NotEnoughFundsException(int fundsRemaining)
        {
            FundsRemaining = fundsRemaining;
        }

        public override string Message { get => 
                $"Not enough funds to spin, ${FundsRemaining} remaining."; }
        private int FundsRemaining { get; }
    }
}
