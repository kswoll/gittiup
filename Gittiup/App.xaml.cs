using System.Windows;
using Movel.Wpf;

namespace Gittiup
{
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            MovelApp.Initialize();
        }
    }
}
