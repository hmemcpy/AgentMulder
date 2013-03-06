![Agent Mulder](http://i.imgur.com/7ZNxO.png)

**Agent Mulder** plugin for ReSharper analyzes DI containers (Dependency Injection, sometimes called Inversion of Control, or IoC containers) in your solution, and provides navigation to and finding usages of types registered or resolved via DI containers.

### To download Agent Mulder, visit http://hmemcpy.github.com/AgentMulder/

## Contributing to Agent Mulder

<a href='http://www.pledgie.com/campaigns/18901'><img alt='Click here to lend your support to: Agent Mulder plugin for ReSharper and make a donation at www.pledgie.com !' src='http://www.pledgie.com/campaigns/18901.png?skin_name=chrome' border='0' /></a>

If you want to contribute to the project, check out the list of open [issues](https://github.com/hmemcpy/AgentMulder/issues).  

You can:

 - raise an issue
 - suggest a feature for the application

If you would like to contribute code to the project:
 
  1. A bit of background reading:
    - [Setting up Git for Windows and connecting to GitHub](http://help.github.com/win-set-up-git/)
    - [The Simple Guide to Git](http://rogerdudler.github.com/git-guide/)
    - [How to GitHub: Fork, Branch, Track, Squash and Pull Request](http://gun.io/blog/how-to-github-fork-branch-and-pull-request/).
  2. Fork the repository ([how-to](http://help.github.com/fork-a-repo/))
  3. Make some changes to the code base
  4. Send us a Pull Request once you're happy with it ([how-to](http://help.github.com/send-pull-requests/))
   
We'll do a bit of a code review before accepting your patch.

### Git Flow

You will notice when you fork the Agent Mulder repository that the default branch is `develop` rather than the more usual `master`.  We use the Git Flow branching model, [first described](http://nvie.com/posts/a-successful-git-branching-model/) by [nvie](http://www.twitter.com/nvie), so Agent Mulder's `master` branch moves on only at specific points, when we're really sure we want to promote something to production.  

**Use of Git Flow is not required for contributing to Agent Mulder**, particularly if you're submitting a bug-fix or small feature.  Its use is recommended for larger changes where `develop` might move on whilst you're completing your work.

#### Configuring Git Flow

There is a set of [helper scripts](https://github.com/nvie/gitflow) that will work on both Unix-based operating systems and Windows.  Follow the appropriate [installation instructions](https://github.com/nvie/gitflow/wiki/Installation) for your operating system, and configure your working copy repository for use with Git Flow by typing `git flow init`.  Accept all the default options to the questions that it asks you.

#### Using Git Flow

Pick a feature or bug to work on and create a new branch for that work by typing `git flow feature start <featurename>`.  This will create you a new *feature branch* for your work called `feature/<featurename>`, and you can use git as usual from this point.  

Once your feature is finished, type `git flow feature publish <featurename>`.  This will copy the *feature branch* to your `origin` repository on GitHub and you will then be able to submit a pull request to have it merged into Agent Mulder's own `develop` branch.  **Note: do not use `git flow feature finish <featurename>`!**  This will automatically merge your *feature branch* back into `develop` and delete the *feature branch*, making it harder for you to submit your pull request.

If you wish to update your published feature branch after the initial publish, use a regular `git push origin feature/<featurename>`.  This will also update your pull request if you have one open for that branch.

If you find Agent Mulder's `develop` branch has moved on, and you need/want to take advantage of the changes made there, you can update your feature branch as follows:

  1. Ensure you have a remote configured for the upstream repository.  You can use `git remote add upstream git://github.com/hmemcpy/AgentMulder.git` to add it if it doesn't already exist.
  2. Type `git pull upstream develop:develop` to update your local repository with the upstream refs.
  3. Type `git flow feature rebase <featurename>` to rebase your feature branch on top of the new `develop`.
  
There is a lot of help available for Git Flow, which can be accessed by typing `git flow feature help`.

<hr/>

## Special thanks ##

![ReSharper](http://www.jetbrains.com/img/logos/logo_resharper_small.gif)  
[ReSharper](http://www.jetbrains.com/resharper/) - the most advanced productivity add-in for Visual Studio!

![Advanced Installer](http://www.caphyon.com/img/press/ai/small-logo.png)  
Agent Mulder's installer is powered by [Advanced Installer](http://www.advancedinstaller.com/) - the easiest way to create powerful MSI-based installers!
