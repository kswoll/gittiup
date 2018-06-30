using System.Windows.Controls;
using Gittiup.Library.ViewModels;

namespace Gittiup.Views
{
    public class StringInputDialogBase : BaseView<StringInputViewModel>
    {
    }

    public partial class StringInputDialog
    {
        public StringInputDialog(StringInputViewModel model)
        {
            InitializeComponent();

            ViewModel = model;
        }
    }
}
