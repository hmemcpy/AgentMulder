using System.Collections.Generic;
using System.Linq;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.ReSharper.Domain.Patterns
{
    public class RegisterWithService : RegistrationPatternBase
    {
        protected RegisterWithService(IStructuralSearchPattern pattern)
            : base(pattern)
        {
        }

        public override IEnumerable<IComponentRegistration> GetComponentRegistrations(ITreeNode registrationRootElement)
        {
            // ReSharper does not currently match generic and non-generic overloads separately, meaning that Register<T> and Register(typeof(T))
            // will be both matched with a single pattern Register($arguments$).
            // Therefire I am using this pattern to look for both generic and non-generic (with typeof) overloads of the pattern

            IStructuralMatchResult match = Match(registrationRootElement);

            if (match.Matched)
            {
                var invocationExpression = match.MatchedElement as IInvocationExpression;
                if (invocationExpression == null)
                {
                    yield break;
                }

                if (invocationExpression.TypeArguments.Any())
                {
                    foreach (var registration in FromGenericArguments(invocationExpression))
                    {
                        yield return registration;
                    }
                }
                else
                {
                    foreach (var registration in FromArguments(invocationExpression))
                    {
                        yield return registration;
                    }
                }
            }
        }

        protected virtual IEnumerable<IComponentRegistration> FromGenericArguments(IInvocationExpression invocationExpression)
        {
            var first = invocationExpression.TypeArguments.First() as IDeclaredType;
            var last = invocationExpression.TypeArguments.Last() as IDeclaredType;

            return CreateRegistration(invocationExpression, first, last);
        }

        private IEnumerable<IComponentRegistration> CreateRegistration(IInvocationExpression invocationExpression, IDeclaredType first, IDeclaredType last)
        {
            if (first == null || last == null)
            {
                yield break;
            }
            
            ITypeElement fromType = first.GetTypeElement();
            ITypeElement toType = last.GetTypeElement();

            if (fromType != null && toType != null)
            {
                yield return fromType.Equals(toType)
                                 ? new ComponentRegistration(invocationExpression, fromType)
                                 : new ComponentRegistration(invocationExpression, fromType, toType);
            }
        }

        protected virtual IEnumerable<IComponentRegistration> FromArguments(IInvocationExpression invocationExpression)
        {
            List<ITypeofExpression> arguments = invocationExpression.ArgumentList.Arguments
                .Where(argument => 
                { 
                    var declaredType = argument.Value.Type() as IDeclaredType;
                    return declaredType != null && declaredType.IsType();
                }).Select(argument => argument.Value as ITypeofExpression)
                .ToList();

            var first = arguments.First().ArgumentType as IDeclaredType;
            var last = arguments.Last().ArgumentType as IDeclaredType;

            return CreateRegistration(invocationExpression, first, last);
        }
    }
}