namespace TestApplication.Types
{
    public class TakesDependency
    {
        private readonly IFoo dependency;

        public TakesDependency(IFoo dependency)
        {
            this.dependency = dependency;
        }
    }
}