using Gittiup.Models;
using Gittiup.ViewModels;

namespace Gittiup.Views
{
    public class EditAccountDialogBase : BaseView<EditAccountViewModel>
    {
    }

    public partial class EditAccountDialog
    {
        public EditAccountDialog(AccountModel account)
        {
            InitializeComponent();

            password.Password = account.Password;
            password.PasswordChanged += (_, __) => account.Password = password.Password;

            ViewModel = new EditAccountViewModel
            {
                Account = account
            };
        }
    }
}
