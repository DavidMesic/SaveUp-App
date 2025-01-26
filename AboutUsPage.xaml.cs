using SaveUpApp.ViewModel;

namespace SaveUpApp;

public partial class AboutUsPage : ContentPage
{
	public AboutUsPage()
	{
		InitializeComponent();
		BindingContext = new AboutUsViewModel();
	}
}