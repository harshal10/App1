
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GamePage : ContentPage
	{
		public GamePage ()
		{
			InitializeComponent ();
		}


        async void ButtonToLeaderboard(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Leaderboard());
        }
    }
}