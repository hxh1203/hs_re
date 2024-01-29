using System.Windows.Controls;

using ArgusCloseLoopTool.Contracts.Views;
using ArgusCloseLoopTool.ViewModels;

using MahApps.Metro.Controls;

namespace ArgusCloseLoopTool.Views;

public partial class ShellWindow : MetroWindow, IShellWindow
{
    public ShellWindow(ShellViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }

    public Frame GetNavigationFrame()
        => shellFrame;

    public void ShowWindow()
        => Show();

    public void CloseWindow()
        => Close();
}
