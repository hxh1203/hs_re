using System.Windows.Controls;

namespace ArgusCloseLoopTool.Contracts.Views;

public interface IShellWindow
{
    Frame GetNavigationFrame();

    void ShowWindow();

    void CloseWindow();
}
