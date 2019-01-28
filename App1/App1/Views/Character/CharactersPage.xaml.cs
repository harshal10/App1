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
	public partial class CharactersPage : ContentPage
	{
        private CharacterViewModel _viewModel;
        public CharactersPage()
		{
			InitializeComponent ();
            BindingContext = _viewModel = CharacterViewModel.Instance;
        }
	
        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Character;
            if (item == null)
                return;

            await Navigation.PushAsync(new CharacterDetailsPage(new CharacterDetailViewModel(item)));

            // Manually deselect item.
            CharacterListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = null;

        if (ToolbarItems.Count > 0)
        {
            ToolbarItems.RemoveAt(0);
        }

        InitializeComponent();

        if (_viewModel.DataSet.Count == 0)
        {
            _viewModel.LoadDataCommand.Execute(null);
        }
    else if (_viewModel.NeedsRefresh())
    {
        _viewModel.LoadDataCommand.Execute(null);
    }

    BindingContext = _viewModel;
}
    }
}