using App1.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : MasterDetailPage
    {
        Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();
        public MainPage()
        {
            InitializeComponent();

            MasterBehavior = MasterBehavior.Popover;

            //Updating default page to About page
            //MenuPages.Add((int)MenuItemType.Browse, (NavigationPage)Detail);
            MenuPages.Add((int)MenuItemType.About, (NavigationPage)Detail);
        }

        public async Task NavigateFromMenu(int id)
        {
            if (!MenuPages.ContainsKey(id))
            {
                switch (id)
                {
                    case (int)MenuItemType.Browse:
                        MenuPages.Add(id, new NavigationPage(new ItemsPage()));
                        break;

                    case (int)MenuItemType.About:
                        MenuPages.Add(id, new NavigationPage(new AboutPage()));
                        break;

                    //adding GamePage
                    case (int)MenuItemType.Game:
                        MenuPages.Add(id, new NavigationPage(new GamePage()));
                        break;

                    //adding Leaderboard
                    case (int)MenuItemType.Leaderbaord:
                        MenuPages.Add(id, new NavigationPage(new LeaderboardPage()));
                        break;

                    //adding Characters
                    case (int)MenuItemType.Characters:
                        MenuPages.Add(id, new NavigationPage(new CharactersPage()));
                        break;

                    //adding items
                    case (int)MenuItemType.Items:
                        MenuPages.Add(id, new NavigationPage(new ItemsPage()));
                        break;
                }
            }

            var newPage = MenuPages[id];

            if (newPage != null && Detail != newPage)
            {
                Detail = newPage;

                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(100);

                IsPresented = false;
            }
        }
    }
}