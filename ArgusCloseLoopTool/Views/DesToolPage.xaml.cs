using System.Windows.Controls;

using ArgusCloseLoopTool.ViewModels;

namespace ArgusCloseLoopTool.Views;

public partial class DesToolPage : Page
{
    public DesToolPage(DesToolViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
