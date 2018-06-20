using System.Linq;
using System.Windows.Controls;
using Gittiup.ViewModels;
using LibGit2Sharp;

namespace Gittiup.Views
{
    public class BranchViewBase : BaseView<BranchViewModel>
    {
    }

    public partial class BranchView
    {
        public BranchView(Branch branch)
        {
            InitializeComponent();

            ViewModel = new BranchViewModel(branch);
//            branch.Commits.ElementAt(0).
        }
    }
}
