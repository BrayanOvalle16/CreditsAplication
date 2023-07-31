namespace CreditsAplication.Api.Exceptions
{
    public class InsuficientBalanceException : Exception
    {
        public InsuficientBalanceException(string message) : base(message)
        {

        }
    }
}
