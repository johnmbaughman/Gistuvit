using CommunityToolkit.Mvvm.ComponentModel;
using Serilog;

namespace GistuvitWPF.ViewModels;

public abstract class ViewModel : ObservableObject
{
    public ILogger Logger { get; }

    protected ViewModel(ILogger logger)
    {
        Logger = logger.ForContext(GetType());
    }
}