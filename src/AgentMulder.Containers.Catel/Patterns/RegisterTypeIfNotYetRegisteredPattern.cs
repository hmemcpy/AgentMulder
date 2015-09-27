// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RegisterTypeIfNotYetRegisteredPattern.cs" company="Catel & Agent Mulder development teams">
//   Copyright (c) 2008 - 2012 Catel & Agent Mulder development teams. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace AgentMulder.Containers.Catel.Patterns
{
    /// <summary>
    /// The register type generic registration pattern.
    /// </summary>
    public class RegisterTypeIfNotYetRegisteredPattern : ServiceLocatorRegistrationPatternBase
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterTypeIfNotYetRegisteredPattern"/> class. 
        /// </summary>
        public RegisterTypeIfNotYetRegisteredPattern()
            : base("RegisterTypeIfNotYetRegistered")
        {
        }

        #endregion
    }
}