using System.Collections.Generic;
using System.ComponentModel.Composition;
using AgentMulder.ReSharper.Domain.Patterns;

namespace AgentMulder.Containers.Autofac.Patterns.Mvc
{
    [Export("ComponentRegistration", typeof(IRegistrationPattern))]
    public class RegisterApiControllers : RegisterControllersBase
    {
        [ImportingConstructor]
        public RegisterApiControllers([ImportMany] IEnumerable<IBasedOnPattern> basedOnPatterns)
            : base("RegisterApiControllers", "System.Web.Http.ApiController", basedOnPatterns)
        {
        }
    }
}