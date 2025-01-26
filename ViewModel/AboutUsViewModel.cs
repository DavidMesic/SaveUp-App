using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaveUpApp.Models;
using SaveUpApp;
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