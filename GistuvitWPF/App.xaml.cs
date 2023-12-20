using Dapplo.Microsoft.Extensions.Hosting.AppServices;
using GistuvitWPF.ViewModels;
using GistuvitWPF.Views;
using Hardcodet.Wpf.TaskbarNotification;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace GistuvitWPF;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App
{
    private readonly IHost _host;
    private TaskbarIcon _notifyIcon;

    public App()
    {
        _host = new HostBuilder()
            .ConfigureSingleInstance("{5df2e6a6-c3da-43e9-88b6-afbb73597883}")
            //.ConfigureCipher()
            //.ConfigureAppConfiguration<Configuration>()
            //.ConfigureLogging()
            .ConfigureServices(ConfigureServices)
            .Build();

        InitializeComponent();

        // How to use the encrypt/decrypt and settings update.

        //var configuration = Services.GetRequiredService<SettingsManager<Configuration>>();
        //configuration.AddOrUpdateAppSetting("Application:OAuthToken", "THisismyoauthkey", true);
        //Log.ForContext<App>().Information(configuration.Values.OAuthToken);
        //Log.ForContext<App>().Information(Services.GetRequiredService<CipherService>().Decrypt(configuration.Values.OAuthToken));
        //Debug.WriteLine(configuration.Values.OAuthToken);
        //configuration.AddOrUpdateAppSetting("Application:OAuthTaken", "THisismyoauthkey");
        //Log.ForContext<App>().Information(configuration.Values.OAuthTaken);
        //Debug.WriteLine(configuration.Values.OAuthTaken);
    }

    /// <summary>
    /// Gets the current.
    /// </summary>
    /// <value>The current.</value>
    //public new static App Current => (App)Application.Current;

    /// <summary>
    /// Gets the name of the application.
    /// </summary>
    /// <value>The name of the application.</value>
    //public string ApplicationName => Assembly.GetExecutingAssembly().FullName ?? "Application name not found";

    /// <summary>
    /// Gets the services.
    /// </summary>
    /// <value>The services.</value>
    public IServiceProvider Services => _host.Services;

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Application.Exit" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.Windows.ExitEventArgs" /> that contains the event data.</param>
    protected override void OnExit(ExitEventArgs e)
    {
        //    _ = Task.Run(async () => await _host.StopAsync(new TimeSpan(0, 0, 30)));
        _notifyIcon.Dispose();
        base.OnExit(e);
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Application.Startup" /> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.StartupEventArgs" /> that contains the event data.</param>
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        _notifyIcon = Services.GetRequiredService<TaskbarIcon>();
    }

    private void ConfigureServices(HostBuilderContext builder, IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton((TaskbarIcon)FindResource("NotifyIcon"));
        //serviceCollection.AddTransient<MainWindowViewModel>();
        //serviceCollection.AddSingleton<MainWindow>();
    }
}