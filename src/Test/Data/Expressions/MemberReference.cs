using System;
using System.Collections.Generic;

namespace TestApplication.Expressions
{
    public class MemberReference
    {
        public MemberReference()
        {
            Predicate<Type> p = t => t.IsClass;
        } 
    }
}