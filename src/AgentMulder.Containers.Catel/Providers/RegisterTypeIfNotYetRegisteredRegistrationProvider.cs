// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RegisterTypeIfNotYetRegisteredRegistrationProvider.cs" company="Catel & Agent Mulder development teams">
//   Copyright (c) 2008 - 2012 Catel & Agent Mulder development teams. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace AgentMulder.Containers.Catel.Providers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;

    using AgentMulder.Containers.Catel.Patterns;
    using AgentMulder.ReSharper.Domain.Patterns;
    using AgentMulder.ReSharper.Domain.Registrations;

    /// <summary>
    /// The register type if not yet registered registration provider.
    /// </summary>
    [Export]
    [Export(typeof(IRegistrationPatternsProvider))]
    public class RegisterTypeIfNotYetRegisteredRegistrationProvider : IRegistrationPatternsProvider
    {
        #region Public Methods and Operators

        /// <summary>
        /// Gets the registration patterns.
        /// </summary>
        /// <returns>
        /// Collection of <see cref="IRegistrationPattern"/>.
        /// </returns>
        public IEnumerable<IRegistrationPattern> GetRegistrationPatterns()
        {
            return new List<IRegistrationPattern>
            {
                new RegisterTypeIfNotYetRegisteredPattern()
            };
        }

        #endregion
    }
}