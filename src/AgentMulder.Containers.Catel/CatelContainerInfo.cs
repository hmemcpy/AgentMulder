// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CatelContainerInfo.cs" company="Catel & Agent Mulder development teams">
//   Copyright (c) 2008 - 2012 Catel & Agent Mulder development teams. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace AgentMulder.Containers.Catel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;
    using System.Linq;
    using System.Reflection;

    using AgentMulder.ReSharper.Domain.Containers;
    using AgentMulder.ReSharper.Domain.Patterns;
    using AgentMulder.ReSharper.Domain.Registrations;

    /// <summary>
    /// The catel container info.
    /// </summary>
    [Export(typeof(IContainerInfo))]
    public class CatelContainerInfo : IContainerInfo
    {
        #region Constants and Fields

        /// <summary>
        /// The registration patterns.
        /// </summary>
        private readonly List<IRegistrationPattern> registrationPatterns;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CatelContainerInfo"/> class. 
        /// </summary>
        public CatelContainerInfo()
        {
            var catalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
            var container = new CompositionContainer(catalog);
            container.ComposeParts(this);
            this.registrationPatterns =
                this.PatternsProviders.SelectMany(provider => provider.GetRegistrationPatterns()).ToList();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the Container display name.
        /// </summary>
        public string ContainerDisplayName
        {
            get
            {
                return "Catel";
            }
        }

        /// <summary>
        /// Gets RegistrationPatterns.
        /// </summary>
        public IEnumerable<IRegistrationPattern> RegistrationPatterns
        {
            get
            {
                return this.registrationPatterns;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets PatternsProviders.
        /// </summary>
        [ImportMany]
        private IEnumerable<IRegistrationPatternsProvider> PatternsProviders { get; set; }

        #endregion
    }
}