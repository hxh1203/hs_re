using System.Windows.Controls;

using ArgusCloseLoopTool.ViewModels;

namespace ArgusCloseLoopTool.Views;

public partial class DataAnalysisToolPage : Page
{
    public DataAnalysisToolPage(DataAnalysisToolViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
