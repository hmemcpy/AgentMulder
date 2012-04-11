using System.Windows.Forms;
using JetBrains.ActionManagement;
using JetBrains.Application.DataContext;

namespace AgentMulder.ReSharper.Plugin
{
  [ActionHandler("AgentMulder.ReSharper.Plugin.About")]
  public class AboutAction : IActionHandler
  {
    public bool Update(IDataContext context, ActionPresentation presentation, DelegateUpdate nextUpdate)
    {
      // return true or false to enable/disable this action
      return true;
    }

    public void Execute(IDataContext context, DelegateExecute nextExecute)
    {
      MessageBox.Show(
        "Agent Mulder Plugin for ReSharper\nIgal Tabachnik\n\nProvides navigation to and finding usages of types, registered or resolved via IoC containers.",
        "About Agent Mulder Plugin for ReSharper",
        MessageBoxButtons.OK,
        MessageBoxIcon.Information);
    }
  }
}
