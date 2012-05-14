using System;

namespace TestApplication.Expressions
{
    public class ConstantExpression
    {
        public ConstantExpression()
        {
            Predicate<Type> p = t => true;
        } 
    }
}