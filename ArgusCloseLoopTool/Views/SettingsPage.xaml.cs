using System.Windows.Controls;

using ArgusCloseLoopTool.ViewModels;

namespace ArgusCloseLoopTool.Views;

public partial class SettingsPage : Page
{
    public SettingsPage(SettingsViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
