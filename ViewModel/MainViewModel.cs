using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaveUpApp.Models;
using SaveUpApp;
using System.Windows.Input;

namespace SaveUpApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public ICommand NavigateToDashboardCommand { get; }
        public ICommand NavigateToAddProductCommand { get; }
        public ICommand NavigateToProductListCommand { get; }
        public ICommand NavigateToAboutUsCommand { get; }


        public MainViewModel()
        {
            NavigateToDashboardCommand = new Command(async () =>
            {
                await Shell.Current.GoToAsync("///DashboardPage");
            });

            NavigateToAddProductCommand = new Command(async () =>
            {
                await Shell.Current.GoToAsync("///AddProductPage");
            });

            NavigateToProductListCommand = new Command(async () =>
            {
                await Shell.Current.GoToAsync("///ProductListPage");
            });

            NavigateToAboutUsCommand = new Command(async () =>
            {
                await Shell.Current.GoToAsync("///AboutUsPage");
            });
        }
    }
}