namespace SimpleWebApp.Domain.Guards
{
    public static class Check
    {
        public static void CheckStringOnNullOrWhiteSpase(string input, string exceptionMessage)
        {
            if(string.IsNullOrWhiteSpace(input))
                throw new ArgumentException(exceptionMessage);
        }

        public static void CheckPriceOnNegativeValue(decimal input, string exceptionMessage)
        { 
            if(input < 0 )
                throw new ArgumentException(exceptionMessage);
        }
    }
}
