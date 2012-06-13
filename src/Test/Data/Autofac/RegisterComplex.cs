using Autofac;
using TestApplication.Types;

namespace TestApplication.Autofac
{
    public class RegisterComplex : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // this example taken as is from the Autofac wiki http://code.google.com/p/autofac/wiki/ComponentCreation

            builder.Register<CreditCard>((c, p) =>
            {
                var accountId = p.Named<string>("accountId");
                if (accountId.StartsWith("9"))
                    return new GoldCard(accountId);
                else
                    return new StandardCard(accountId);
            });
        }
    }
}