using System.Collections.Generic;
using System.ComponentModel.Composition;
using AgentMulder.ReSharper.Domain.Patterns;

namespace AgentMulder.Containers.Autofac.Patterns.Mvc
{
    [Export("ComponentRegistration", typeof(IRegistrationPattern))]
    public class RegisterMvcControllers : RegisterControllersBase
    {
        [ImportingConstructor]
        public RegisterMvcControllers([ImportMany] IEnumerable<IBasedOnPattern> basedOnPatterns)
            : base("RegisterControllers", "System.Web.Mvc.IController", basedOnPatterns)
        {
        }
    }
}