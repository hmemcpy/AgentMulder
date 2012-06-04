using Microsoft.Practices.Unity;
using TestApplication.Types;

namespace TestApplication.Unity
{
    public class RegisterTypeGenericFromTo2Times
    {
        public RegisterTypeGenericFromTo2Times()
        {
            var container = new UnityContainer();
            container.RegisterType<ICommon, CommonImpl1>()
                .RegisterType<ICommon2, CommonImpl12>();
        }
    }
}