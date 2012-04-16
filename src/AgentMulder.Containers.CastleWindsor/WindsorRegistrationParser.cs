using System;
using System.Collections.Generic;
using System.Linq;
using AgentMulder.Core;
using AgentMulder.Core.TypeSystem;
using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.CSharp.Resolver;
using ICSharpCode.NRefactory.PatternMatching;
using ICSharpCode.NRefactory.Semantics;

namespace AgentMulder.Containers.CastleWindsor
{
    public class WindsorRegistrationParser : IRegistrationParser
    {
        private readonly ContainerInvocationVisitor visitor;
        private CSharpAstResolver resolver;
        private readonly List<Registration> registrations = new List<Registration>();

        public WindsorRegistrationParser()
        {
            visitor = new ContainerInvocationVisitor(this);
        }

        public IEnumerable<Registration> Parse(IProject project)
        {
            foreach (IFile file in project.Files)
            {
                resolver = new CSharpAstResolver(project.Compilation, file.CompilationUnit, file.CompilationUnit.ToTypeSystem());
                file.CompilationUnit.AcceptVisitor(visitor);
            }

            return registrations;
        }

        public bool ParseInvocation(InvocationExpression invocationExpression)
        {
            var pattern = new IdentifierExpression("container").Invoke("Register",
                new IdentifierExpression("Component")
                    .Invoke("For", new AstType[] { new AnyNode("for") }, new Expression[0])
                    .Invoke("ImplementedBy", new AstType[] { new AnyNode("implementedBy") }, new Expression[0])
                );

            //var pattern2 = new IdentifierExpression("container").Invoke("Register",
            //    new IdentifierExpression("Component")
            //        .Invoke("For", new TypeOfExpression(new AnyNode("for")))
            //        .Invoke("ImplementedBy", new TypeOfExpression(new AnyNode("implementedBy")))
            //);

            var match = pattern.Match(invocationExpression);
            //var match2 = pattern2.Match(invocationExpression);
            if (match.Success) 
            {
                AstType implementedByType = match.Get<AstType>("implementedBy").Single();

                ResolveResult implBy = resolver.Resolve(implementedByType);

                registrations.Add(new Registration(implBy.Type.FullName));
            }

            //if (match2.Success)
            //{
            //    AstType forType = match2.Get<AstType>("for").Single();
            //    AstType implementedByType = match2.Get<AstType>("implementedBy").Single();

            //    ResolveResult forResult = resolver.Resolve(forType);
            //    ResolveResult implBy = resolver.Resolve(implementedByType);

            //    registeredTypes.Add(forResult.Type.FullName);
            //    registeredTypes.Add(implBy.Type.FullName);
            //}

            return match.Success;
        }
    }
}