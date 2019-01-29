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
	public partial class CharacterEditPage : ContentPage
	{
        private CharacterDetailViewModel _viewModel;

        public Models.Character Data { get; set; }
        public CharacterEditPage (CharacterDetailViewModel viewModel)
		{
            Data = viewModel.Data;

            viewModel.Title = "Edit " + viewModel.Title;

            InitializeComponent();

            BindingContext = _viewModel = viewModel;

        }

        private async void Save_Clicked(object sender, EventArgs e)
        { 
            MessagingCenter.Send(this, "EditData", Data);

            // removing the old CharacterDetails page, 2 up counting this page
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);

            // Add a new character details page, with the new Item data on it
            await Navigation.PushAsync(new CharacterDetailsPage(new CharacterDetailViewModel(Data)));

            // Last, remove this page
            Navigation.RemovePage(this);
        }

        // Cancel and go back a page in the navigation stack
        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

    }
}