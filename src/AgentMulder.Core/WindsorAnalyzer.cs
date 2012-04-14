using System.Collections.Generic;
using System.Linq;
using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.CSharp.Resolver;
using ICSharpCode.NRefactory.ConsistencyCheck;
using ICSharpCode.NRefactory.PatternMatching;
using ICSharpCode.NRefactory.Semantics;

namespace AgentMulder.Core
{
    public class WindsorAnalyzer : IContainerAnalyzer
    {
        private readonly ContainerInvocationVisitor visitor;
        private CSharpAstResolver resolver;

        public WindsorAnalyzer()
        {
            visitor = new ContainerInvocationVisitor(this);
        }

        public void Analyze(CSharpProject project)
        {
            foreach (var file in project.Files)
            {
                resolver = new CSharpAstResolver(project.Compilation, file.CompilationUnit, file.ParsedFile);
                file.CompilationUnit.AcceptVisitor(visitor);
            }
        }

        private readonly HashSet<string> registeredTypes = new HashSet<string>();

        public IEnumerable<string> RegisteredTypes
        {
            get { return registeredTypes; }
        }

        public bool IsContainerInvocation(InvocationExpression invocationExpression)
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
                AstType forType = match.Get<AstType>("for").Single();
                AstType implementedByType = match.Get<AstType>("implementedBy").Single();

                ResolveResult forResult = resolver.Resolve(forType);
                ResolveResult implBy = resolver.Resolve(implementedByType);

                //registeredTypes.Add(forResult.Type.FullName);
                registeredTypes.Add(implBy.Type.FullName);
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