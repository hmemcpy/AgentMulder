namespace TestApplication.Types
{
    public class PrimitiveArgument : IPrimitiveArgument
    {
        private readonly int number;

        public PrimitiveArgument(int number)
        {
            this.number = number;
        }
    }
}