using System;

namespace TestApplication.Expressions
{
    public class MethodCallWithArguments
    {
        public MethodCallWithArguments()
        {
            Predicate<Type> p = t => typeof(int).IsAssignableFrom(t);
        } 
    }
}