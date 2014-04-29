using System.Collections.Generic;
using System.Linq;
using JetBrains.Application.Progress;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Search;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Util;

namespace AgentMulder.Containers.StructureMap.Registrations
{
    public class SingleImplementationsOfInterfaceConvention : StructureMapConvention
    {
        private class SingleImplementationChecker : IFindResultConsumer<bool>
        {
            private readonly HashSet<FindResult> results = new HashSet<FindResult>();

            public bool IsSingleImplementation
            {
                get { return results.Count == 1; }
            }

            public bool Build(FindResult result)
            {
                results.Add(result);
                return true;
            }

            public FindExecution Merge(bool data)
            {
                return FindExecution.Continue;
            }
        }

        public SingleImplementationsOfInterfaceConvention(ITreeNode registrationRootElement)
            : base(registrationRootElement)
        {
            AddFilter(element =>
            {
                var firstInterface = element.GetSuperTypes()
                                            .SelectNotNull(type => type.GetTypeElement())
                                            .OfType<IInterface>()
                                            .FirstOrDefault();
                if (firstInterface == null)
                {
                    return false;
                }

                var singleInheritorChecker = new SingleImplementationChecker();

                element.GetPsiServices().Finder.FindInheritors(firstInterface, element.GetSearchDomain(), singleInheritorChecker, NullProgressIndicator.Instance);

                return singleInheritorChecker.IsSingleImplementation;
            });
        }
    }
}