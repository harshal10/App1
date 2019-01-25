using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using App1.Models;
using App1.Views;
using System.Linq;

namespace App1.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private static ItemsViewModel _instance;

        public static ItemsViewModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ItemsViewModel();
                }
                return _instance;
            }
        }

        public ObservableCollection<Item> DataSet { get; set; }
        public Command LoadDataCommand { get; set; }
        private bool _needsRefresh;


        public ItemsViewModel()
        {
            Title = "Item List";
            DataSet = new ObservableCollection<Item>();
            LoadDataCommand = new Command(async () => await ExecuteLoadItemsCommand());
            /*
            #region Messages
            MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as Item;
                DataSet.Add(newItem);
                await DataStore.AddAsync_Item(newItem);
            });
            #endregion
            */
        }
        // refresh check
        public bool NeedsRefresh()
        {
            if (_needsRefresh)
            {
                _needsRefresh = false;
                return true;
            }

            return false;
        }


        // refresh
        public void SetNeedsRefresh(bool value)
        {
            _needsRefresh = value;
        }

        private async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                DataSet.Clear();
                var dataset = await DataStore.GetAllAsync_Item(true);
                dataset = dataset
                    .OrderBy(a => a.Id)
                    .ThenBy(a => a.Name)
                    .ThenBy(a => a.Description)
                  
                    .ToList();
                foreach (var data in dataset)
                {
                    DataSet.Add(data);
                }
            
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
        public void ForceDataRefresh()
        {
            // Reset
            var canExecute = LoadDataCommand.CanExecute(null);
            LoadDataCommand.Execute(null);
        }
    }
}