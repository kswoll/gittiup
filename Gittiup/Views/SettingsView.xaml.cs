using System.Windows.Controls;
using Gittiup.Library.Stores;

namespace Gittiup.Views
{
    public partial class SettingsViewBase : BaseView<SettingsStore>
    {
    }

    public partial class SettingsView
    {
        public SettingsView()
        {
            InitializeComponent();
        }
    }
}
