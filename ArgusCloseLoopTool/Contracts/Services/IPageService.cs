using System.Windows.Controls;

namespace ArgusCloseLoopTool.Contracts.Services;

public interface IPageService
{
    Type GetPageType(string key);

    Page GetPage(string key);
}
