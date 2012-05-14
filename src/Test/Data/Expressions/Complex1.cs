using System;
using System.Collections.Generic;

namespace TestApplication.Expressions
{
    public class Complex1
    {
        public Complex1()
        {
            Predicate<Type> p = t => !(t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IEnumerable<>));
        } 
    }
}