using App1.Models;
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
	public partial class CharacterNewPage : ContentPage
	{
        public Character Data { get; set; }
        public CharacterNewPage ()
		{
			InitializeComponent ();
            Data = new Character
            {
                Name = "Character Name",
                Description = "Special powers...",
                Age = 10
            };
            BindingContext = this;
        }

        // Respond to the Save click
        // Send the add message to so it gets added...
        private async void Save_Clicked(object sender, EventArgs e)
        {    
            MessagingCenter.Send(this, "AddData", Data);
            await Navigation.PopModalAsync();
        }

        // Cancel and go back a page in the navigation stack
        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}
