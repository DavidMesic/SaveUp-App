using SaveUpApp.Interfaces;

namespace SaveUpApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            DependencyService.Register<IAlertService, AlertService>();

            MainPage = new AppShell();
        }
    }
}