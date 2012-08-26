// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RegisterTypePattern.cs" company="Catel & Agent Mulder development teams">
//   Copyright (c) 2008 - 2012 Catel & Agent Mulder development teams. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace AgentMulder.Containers.Catel.Patterns
{
    /// <summary>
    /// The register type non generic registration pattern.
    /// </summary>
    public class RegisterTypePattern : ServiceLocatorRegistrationPatternBase
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterTypePattern"/> class. 
        /// </summary>
        public RegisterTypePattern()
            : base("RegisterType")
        {
        }

        #endregion
    }
}