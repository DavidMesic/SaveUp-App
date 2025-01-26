using System.Windows.Input;

namespace SaveUpApp.ViewModel;

public class AboutUsViewModel : ContentView
{
    public ICommand NavigateBackCommand { get; }

    public AboutUsViewModel()
	{
        NavigateBackCommand = new Command(async () =>
        {
            await Shell.Current.GoToAsync("///MainPage");
        });
    }
}