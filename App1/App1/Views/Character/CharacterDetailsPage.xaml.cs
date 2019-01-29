using App1.Models;
using App1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CharacterDetailsPage : ContentPage
	{
        CharacterDetailViewModel _viewModel;
		public CharacterDetailsPage (CharacterDetailViewModel charViewModel)
		{
			InitializeComponent ();
            BindingContext = _viewModel = charViewModel;
        }

        public CharacterDetailsPage()
        {
            InitializeComponent();

            var data = new Character();

            _viewModel = new CharacterDetailViewModel(data);
            BindingContext = _viewModel;
        }

        //private async void Edit_Clicked(object sender, EventArgs e)
        //{
        //    await Navigation.PushAsync(new CharacterEditPage(_viewModel));
        //}

        private async void Delete_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CharacterDeletePage(_viewModel));
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}