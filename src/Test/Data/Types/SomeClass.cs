namespace TestApplication.Types
{
    public class SomeClass
    {
        public Bindable Bind<T>()
        {
            return new Bindable();
        }

        public Bindable For<T>()
        {
            return new Bindable();
        }
    }

    public class Bindable
    {
        public void To<T>()
        {
        }
        
        public void Use<T>()
        {
        }
    }
}