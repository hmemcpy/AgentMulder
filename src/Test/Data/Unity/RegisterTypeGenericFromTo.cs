using Microsoft.Practices.Unity;
using TestApplication.Types;

namespace TestApplication.Unity
{
    public class RegisterTypeGenericFromTo
    {
        public RegisterTypeGenericFromTo()
        {
            var container = new UnityContainer();
            container.RegisterType<ICommon, CommonImpl1>();
        } 
    }
}