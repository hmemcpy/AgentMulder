using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Security;
using System.Security.Policy;
using System.Xml.Linq;
using ExpressionSerialization;

namespace AgentMulder.ReSharper.Domain.Utils
{
    /// <summary>
    /// Loads an assembly into a new AppDomain and obtains all the
    /// namespaces in the loaded Assembly, which are returned as a 
    /// List. The new AppDomain is then Unloaded.
    /// 
    /// This class creates a new instance of a 
    /// <c>AssemblyLoader</c> class
    /// which does the actual ReflectionOnly loading 
    /// of the Assembly into
    /// the new AppDomain.
    /// </summary>
    public class SeperateAppDomainAssemblyLoader
    {
        /// <summary>
        /// Loads an assembly into a new AppDomain and obtains all the
        /// namespaces in the loaded Assembly, which are returned as a 
        /// List. The new AppDomain is then Unloaded
        /// </summary>
        /// <param name="assemblyLocation">The Assembly file 
        /// 	location</param>
        /// <param name="predicateString"> </param>
        /// <returns>A list of found namespaces</returns>
        public IEnumerable<string> GetTypesFromAssembly(FileInfo assemblyLocation, string predicateString)
        {
            AppDomain childDomain = BuildChildDomain(AppDomain.CurrentDomain);

            try
            {
                Type loaderType = typeof(AssemblyLoader);
                var loader = (AssemblyLoader)childDomain.CreateInstanceFrom(loaderType.Assembly.Location, loaderType.FullName).Unwrap();

                loader.LoadAssembly(assemblyLocation.FullName);

                return loader.GetTypes(assemblyLocation.Directory.FullName, predicateString);
                
            }
            finally
            {
                if (childDomain != null)
                {
                    AppDomain.Unload(childDomain);
                }
            }
        }

        #region Private Methods

        /// <summary>
        /// Creates a new AppDomain based on the parent AppDomains 
        /// Evidence and AppDomainSetup
        /// </summary>
        /// <param name="parentDomain">The parent AppDomain</param>
        /// <param name="assemblyLocation"> </param>
        /// <returns>A newly created AppDomain</returns>
        private AppDomain BuildChildDomain(AppDomain parentDomain)
        {
            var evidence = new Evidence(parentDomain.Evidence);
            var setup = new AppDomainSetup
            {
                ApplicationBase = parentDomain.SetupInformation.ApplicationBase
            };
            return AppDomain.CreateDomain("DiscoveryRegion", evidence, setup);
        }

        #endregion

        private class AssemblyLoader : MarshalByRefObject
        {
            public IEnumerable<string> GetTypes(string path, string predicateString)
            {
                var directory = new DirectoryInfo(path);
                ResolveEventHandler resolveEventHandler = (s, e) => OnReflectionOnlyResolve(e, directory);

                AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += resolveEventHandler;

                Assembly reflectionOnlyAssembly = AppDomain.CurrentDomain.ReflectionOnlyGetAssemblies().First();

                Type[] types = reflectionOnlyAssembly.GetTypes();

                ExpressionSerialization.ExpressionSerializer ser = new ExpressionSerializer();
                XElement element = XElement.Parse(predicateString);
                Expression<Predicate<Type>> deserialize = ser.Deserialize<Predicate<Type>>(element);
                Predicate<Type> p = deserialize.Compile();

                AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve -= resolveEventHandler;
                
                return types.Where(t => p(t)).Select(t => t.FullName).ToArray();
            }

            /// <summary>
            /// Attempts ReflectionOnlyLoad of current 
            /// Assemblies dependants
            /// </summary>
            /// <param name="args">ReflectionOnlyAssemblyResolve 
            /// event args</param>
            /// <param name="directory">The current Assemblies 
            /// Directory</param>
            /// <returns>ReflectionOnlyLoadFrom loaded
            /// dependant Assembly</returns>
            private Assembly OnReflectionOnlyResolve(ResolveEventArgs args, DirectoryInfo directory)
            {
                Assembly loadedAssembly = AppDomain.CurrentDomain.ReflectionOnlyGetAssemblies().FirstOrDefault(
                    asm => string.Equals(asm.FullName, args.Name, StringComparison.OrdinalIgnoreCase));

                if (loadedAssembly != null)
                {
                    return loadedAssembly;
                }

                var assemblyName =
                    new AssemblyName(args.Name);
                string dependentAssemblyFilename = Path.Combine(directory.FullName, assemblyName.Name + ".dll");

                if (File.Exists(dependentAssemblyFilename))
                {
                    return Assembly.ReflectionOnlyLoadFrom(dependentAssemblyFilename);
                }
                return Assembly.ReflectionOnlyLoad(args.Name);
            }


            /// <summary>
            /// ReflectionOnlyLoad of single Assembly based on 
            /// the assemblyPath parameter
            /// </summary>
            /// <param name="assemblyPath">The path to the Assembly</param>
            [SuppressMessage("Microsoft.Performance",
                "CA1822:MarkMembersAsStatic")]
            internal void LoadAssembly(String assemblyPath)
            {
                try
                {
                    Assembly.ReflectionOnlyLoadFrom(assemblyPath);
                }
                catch (FileNotFoundException)
                {
                    /* Continue loading assemblies even if an assembly
                     * can not be loaded in the new AppDomain. */
                }
            }
        }
    }
}