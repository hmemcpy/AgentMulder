// Patterns: 1
// Matches: GoldCard.cs
// NotMatches: StandardCard.cs

using System.Linq;
using Autofac;
using TestApplication.Types;

namespace TestApplication.Autofac
{
    public class RegisterComplexWithVariable : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // this example taken as is from the Autofac wiki http://code.google.com/p/autofac/wiki/ComponentCreation

            builder.Register<CreditCard>((c, p) =>
            {
                var result = new GoldCard("1");
                if (p.Any())
                {
                    result.SomeProperty = "X";
                }
                return result;
            });
        }
    }
}