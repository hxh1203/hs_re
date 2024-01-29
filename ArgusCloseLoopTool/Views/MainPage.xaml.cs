using System.Windows.Controls;

using ArgusCloseLoopTool.ViewModels;

namespace ArgusCloseLoopTool.Views;

public partial class MainPage : Page
{
    public MainPage(MainViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
