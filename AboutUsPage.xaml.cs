using SaveUpApp.ViewModel;
using SaveUpApp.ViewModels;

namespace SaveUpApp;

public partial class AboutUsPage : ContentPage
{
	public AboutUsPage()
	{
		InitializeComponent();
		BindingContext = new AboutUsViewModel();
	}
}