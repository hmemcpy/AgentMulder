// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CatelContainerInfo.cs" company="Catel & Agent Mulder development teams">
//   Copyright (c) 2008 - 2012 Catel & Agent Mulder development teams. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Reflection;
using AgentMulder.ReSharper.Domain.Containers;

namespace AgentMulder.Containers.Catel
{
    using System;

    [Export(typeof(IContainerInfo))]
    public class CatelContainerInfo : ContainerInfoBase
    {
        public override string ContainerDisplayName
        {
            get { return "Catel IoC"; }
        }

        public override IEnumerable<string> ContainerQualifiedNames
        {
            get { yield return "Catel.IoC"; }
        }

        protected override ComposablePartCatalog GetComponentCatalog()
        {
            return new AssemblyCatalog(Assembly.GetExecutingAssembly());
        }
    }
}