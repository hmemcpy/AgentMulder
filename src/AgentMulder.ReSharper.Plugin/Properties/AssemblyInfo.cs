using System.Reflection;
using System.Runtime.CompilerServices;
using JetBrains.Application.PluginSupport;
using JetBrains.UI;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("AgentMulder.ReSharper.Plugin")]

// The following information is displayed by ReSharper in the Plugins dialog
[assembly: PluginTitle("Agent Mulder plugin for ReSharper")]
[assembly: PluginDescription("Provides navigation to and finding usages of types registered or resolved via Dependency Injection (DI) containers.\r\n"+
                             "Copyright © Igal Tabachnik, 2012")]
[assembly: PluginVendor("Igal Tabachnik")]

[assembly: InternalsVisibleTo("AgentMulder.ReSharper.Tests")]