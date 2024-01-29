using System.Windows.Controls;

using ArgusCloseLoopTool.ViewModels;

namespace ArgusCloseLoopTool.Views;

public partial class UserRegistrationToolPage : Page
{
    public UserRegistrationToolPage(UserRegistrationToolViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
