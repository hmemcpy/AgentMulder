![Agent Mulder](http://i.imgur.com/7ZNxO.png)

**Agent Mulder** plugin for ReSharper analyzes DI containers (Dependency Injection, sometimes called Inversion of Control, or IoC containers) in your solution, and provides navigation to and finding usages of types registered or resolved via DI containers.

# Check out the sneak preview video here: https://vimeo.com/41113265 #
(GitHub does not allow embedding videos in the README page)

## The Problem ##

With all the major benefits of using smart DI containers to wire up dependencies in code, it breaks automatic code-analysis tools like ReSharper, because DI containers will only create concrete types at run-time (typically using Reflection), and this information is not known during static code analysis.

Consider the following example with [Castle Windsor](http://www.castleproject.org/container/index.html):

```csharp
var container = new WindsorContainer();
container.Register(
    Component.For<IMessageWriter>().ImplementedBy<ConsoleMessageWriter>()
    );
    
var messageWriter = container.Resolve<IMessageWriter>();
```    

Using the configuration above, at runtime the container will resolve the concrete `ConsoleMessageWriter`, however, if you're using ReSharper with solution-wide analysis enabled, it will tell you that `ConsoleMessageWriter` is never instantiated:

![Never instantiated](http://i.imgur.com/YNWby.png)

This gets worse in convention-based registrations, such as:

```csharp
container.Register(
    Classes.FromThisAssembly().BasedOn<IMessageWriter>()
    );
```

Where ReSharper will not even know that this type is being used:

![Never used](http://i.imgur.com/pSezv.png)

**Agent Mulder** aims to solve that problem!

## The Solution ##

Agent Mulder plugin for ReSharper analyzes known DI container registrations in the entire solution, and adds the missing information to ReSharper, so it no longer flags concrete types as being unused. It even adds an visual icon (![Black Magic Hat](http://i.imgur.com/QOZr1.png)) to the type name, allowing you to navigate to the exact line, where the concrete type is being implicitly or explicitly registered:

![Agent Mulder](http://i.imgur.com/xjYrT.png)

## Building and installing Agent Mulder plugin ##

To build the plugin you need to have the [ReSharper SDK](http://www.jetbrains.com/resharper/download/) (v6.1.x at the time of writing), installed on your machine. The project will add the references automatically.

To build the plugin, run the file `src\build.cmd`. The files will be built and placed into the directories `output\Debug` and `output\Release`. In the directory will also be a batch file that copies the files into the ReSharper's plugin directory. You will need to restart Visual Studio to see the changes.

## Frequently Asked Questions (April 25th, 2012)##

**Q: Wow! How does it work?**

**A:** Agent Mulder makes heavy use ReSharper's [Structural Search](http://www.jetbrains.com/resharper/webhelp/Navigation_and_Search__SSR__Searching_for_Code_with_Pattern.html) to look for registration patterns, such as `Component.For<$service$>()`. This makes searching very efficient and does not require any additional parsing. This also allows for adding new patterns easily, allowing adding support for additional DI containers.

**Q: What DI containers are currently supported?**

**A:** The list of the currently supported DI containers and their syntax can be found on the [wiki page](https://github.com/hmemcpy/AgentMulder/wiki).

**Q: What about X (Ninject/StructureMap/Unity)? Can you add support for it?**

**A:** Great question! Suggest a feature on the [issue tracker](https://github.com/hmemcpy/AgentMulder/issues), or better yet, send me a pull request!

**Q: Why the name Agent Mulder?**

**A:** There already exist a few great plugins for ReSharper called Agent Smith, Agent Johnson and Agent Ralph. I decided to continue the tradition, and after some consideration I decided on Agent Mulder - *The IoC Investigator* :)

**Q: I found a bug/Agent Mulder highlights the wrong type (or doesn't)/It doesn't work!**

**A:** Great! [Let me know](https://github.com/hmemcpy/AgentMulder/issues) about it, and I will try to fix it! Please note that I don't know all DI containers and their rules - if you think a mistake in analysis has been made, please note what should be the desired outcome in the issue.

Also please note that Agent Mulder plugin requires ReSharper 6.1 and only works in Visual Studio 2010. Other versions were not tested!

Happy Investigating!