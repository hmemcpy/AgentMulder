using System;
using System.Collections.Generic;
using JetBrains.ReSharper.Psi;

namespace AgentMulder.ReSharper.Plugin.Navigation
{
    public class RegisteredComponentsQuery : IRegisteredComponentsQuery
    {
         
    }

    public interface IRegisteredComponentsQuery : IEnumerable<IType>
    {
        IEnumerable<string> Names { get; }

        bool HasExtensionMethods(string name, bool caseSensitive);

        IRegisteredComponentsQuery ByName(string name, bool caseSensitive);

        IRegisteredComponentsQuery CandidatesForType(IType type);

        IRegisteredComponentsQuery OfNamespace(IEnumerable<INamespace> namespaces);

        IRegisteredComponentsQuery OfNamespace(INamespace @namespace);

        IRegisteredComponentsQuery CandidatesApplicableForType(IType type, PsiLanguageType language, ITypeConversionRule typeConversionRule);

        IRegisteredComponentsQuery FilterBy(Func<IMethod, bool> predicate);
    }

}