using ArgusCloseLoopTool.Models;

namespace ArgusCloseLoopTool.Contracts.Services;

public interface IThemeSelectorService
{
    void InitializeTheme();

    void SetTheme(AppTheme theme);

    AppTheme GetCurrentTheme();
}
