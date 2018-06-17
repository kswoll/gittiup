using System.Windows.Controls;
using Gittiup.Models;
using Gittiup.ViewModels;

namespace Gittiup.Views
{
    public class EditRepositoryDialogBase : BaseView<EditRepositoryViewModel>
    {
    }

    public partial class EditRepositoryDialog
    {
        public EditRepositoryDialog(RepositoryModel repository)
        {
            InitializeComponent();

            ViewModel = new EditRepositoryViewModel(repository);
        }
    }
}
