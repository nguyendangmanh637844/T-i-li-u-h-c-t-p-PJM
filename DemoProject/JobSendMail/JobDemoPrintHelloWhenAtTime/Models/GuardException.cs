namespace BackgroundJobExample.CustomerException
{
    public sealed class GuardException : Exception
    {
        public string ArgumentName { get; }

        public GuardException(string argumentName, string message)
          : base(message)
        {
            ArgumentName = argumentName;
        }
    }
}