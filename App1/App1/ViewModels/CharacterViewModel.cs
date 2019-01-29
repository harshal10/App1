using System;
using System.Collections.Generic;
using System.Text;
using App1.Models;
using App1.Views;
using System.Linq;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Diagnostics;

namespace App1.ViewModels
{
    public class CharacterViewModel : BaseViewModel
    {
        private static CharacterViewModel _instance;

        public static CharacterViewModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CharacterViewModel();
                }
                return _instance;
            }
        }

        public ObservableCollection<Character> DataSet { get; set; }
        public Command LoadDataCommand { get; set; }
        private bool _needsRefresh;


        public CharacterViewModel()
        {
            Title = "Character List";
            DataSet = new ObservableCollection<Character>();
            LoadDataCommand = new Command(async () => await ExecuteLoadCharCommand());

            #region Messages
            MessagingCenter.Subscribe<CharacterDeletePage, Character>(this, "DeleteData", async (obj, data) =>
            {
                await DeleteAsync(data);
            });

            MessagingCenter.Subscribe<CharacterNewPage, Character>(this, "AddData", async (obj, data) =>
            {
                await AddAsync(data);
            });

            MessagingCenter.Subscribe<CharacterEditPage, Character>(this, "EditData", async (obj, data) =>
            {
                await UpdateAsync(data);
            });

            #endregion Messages

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
        private async Task ExecuteLoadCharCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                DataSet.Clear();
                var dataset = await DataStore.GetAllAsync_Char(true);
                dataset = dataset
                    .OrderBy(a => a.Age)
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
        //Data operations
        public async Task<bool> UpdateAsync(Character data)
        {
            var myData = DataSet.FirstOrDefault(arg => arg.Name == data.Name);
            if (myData == null)
            {
                return false;
            }

            myData.Update(data);
            await DataStore.UpdateAsync_Character(myData);

            _needsRefresh = true;

            return true;
        }

        public async Task<bool> AddAsync(Character data)
        {
            DataSet.Add(data);
            var myReturn = await DataStore.AddAsync_Character(data);
            return myReturn;
        }

        private async Task<bool> DeleteAsync(Character data)
        {
            DataSet.Remove(data); 
            var myReturn = await DataStore.DeleteAsync_Character(data);
            return myReturn;
        }
    }
}
