using System;
using System.Linq;
using System.Collections.Generic;

namespace TestApplication.Expressions
{
    public class Complex2
    {
        public Complex2()
        {
            Predicate<Type> p = t => t.GetGenericArguments().Length > 1;
        }
    }
}