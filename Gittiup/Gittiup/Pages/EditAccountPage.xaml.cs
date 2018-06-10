using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Gittiup.Database;
using Gittiup.ViewModels;

namespace Gittiup.Pages
{
    public class EditAccountPageBase : BasePage<EditAccountViewModel>
    {
    }

    public sealed partial class EditAccountPage
    {
        public EditAccountPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            ViewModel = (EditAccountViewModel)e.Parameter;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Save();

            Frame.GoBack();
        }
    }
}
