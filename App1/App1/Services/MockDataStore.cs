﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App1.Models;
using App1.ViewModels;

namespace App1.Services
{
    public class MockDataStore : IDataStore
    {
        
        private static MockDataStore _instance;
        public static MockDataStore Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MockDataStore();
                }
                return _instance;
            }
        }
        

        private List<Item> _itemDataset = new List<Item>();

        private List<Character> _charDataset = new List<Character>();

        private MockDataStore()
        {
            InitilizeSeedData();
        }


        private void InitilizeSeedData()
        {
            //new item
            _itemDataset.Add(new Item("Armour", "Heavy duty protection against big hits.",
                "http://www.clipartbest.com/cliparts/7Ta/6qq/7Ta6qqbEc.png"));

            _itemDataset.Add(new Item("Lightning", "Strikes oppenet with force.",
                "http://www.clipartbest.com/cliparts/4Tb/4yM/4Tb4yMrac.gif"));

            _itemDataset.Add(new Item("Fox", "Cunning fox creates diversion " +
                "for player to attack the monster.",
                "http://www.clipartbest.com/cliparts/niE/Eze/niEEze5BT.jpg"));

            _itemDataset.Add(new Item("Sorcer's Stone", "Makes the player " +
                "unbeatable for next two moves",
                "https://img1.etsystatic.com/051/1/10757639/il_fullxfull.742129741_1iqy.jpg"));

            _itemDataset.Add(new Item("Hippogriff", "Fierce fighting partner that helps player" +
                "fly around the monster attacks.",
                "http://www.hogwartsprofessor.com/wp-content/uploads/2011/11/hippogriff.jpg"));

            //dummy data for character

            _charDataset.Add(new Character("Harry", "Scar faced, dragon tattoed, demontor slayer",
                13));

            _charDataset.Add(new Character("Hermoine", "If knowledge could kill...",
                14));

            _charDataset.Add(new Character("Rom", "the sidekick", 15));
        }

        private void CreateTables()
        {
            // Do nothing...
        }

        // Delete the Datbase Tables by dropping them
        public void DeleteTables()
        {
            // Implement
        }

        // Tells the View Models to update themselves.
        private void NotifyViewModelsOfDataChange()
        {
            CharacterViewModel.Instance.SetNeedsRefresh(true);
        }

        public void InitializeDatabaseNewTables()
        {
            DeleteTables();

            // make them again
            CreateTables();

            // Populate them
            InitilizeSeedData();

            // Tell View Models they need to refresh
            NotifyViewModelsOfDataChange();
        }
        //Item
        #region Item
        public async Task<bool> InsertUpdateAsync_Item(Item data)
        {

            // Check to see if the item exist
            var oldData = await GetAsync_Item(data.Id);
            if (oldData == null)
            {
                _itemDataset.Add(data);
                return true;
            }

            // Compare it, if different update in the DB
            var UpdateResult = await UpdateAsync_Item(data);
            if (UpdateResult)
            {
                await AddAsync_Item(data);
                return true;
            }

            return false;
        }

        public async Task<bool> AddAsync_Item(Item data)
        {
            _itemDataset.Add(data);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateAsync_Item(Item data)
        {
            var myData = _itemDataset.FirstOrDefault(arg => arg.Id == data.Id);
            if (myData == null)
            {
                return false;
            }

            //  myData.Update(data);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync_Item(Item data)
        {
            var myData = _itemDataset.FirstOrDefault(arg => arg.Id == data.Id);
            _itemDataset.Remove(myData);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetAsync_Item(string id)
        {
            return await Task.FromResult(_itemDataset.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetAllAsync_Item(bool forceRefresh = false)
        {
            return await Task.FromResult(_itemDataset);
        }
        #endregion Item
        //Character
        #region Character
        public async Task<bool> AddAsync_Character(Character data)
        {
            _charDataset.Add(data);

            return await Task.FromResult(true);
        }
        public async Task<bool> UpdateAsync_Character(Character data)
        {
            var myData = _charDataset.FirstOrDefault(arg => arg.Name == data.Name);
            if (myData == null)
            {
                return false;
            }

            myData.Update(data);

            return await Task.FromResult(true);
        }
        public async Task<bool> DeleteAsync_Character(Character data)
        {
            var myData = _charDataset.FirstOrDefault(arg => arg.Name == data.Name);
            _charDataset.Remove(myData);

            return await Task.FromResult(true);
        }
        public async Task<IEnumerable<Character>> GetAllAsync_Char(bool forceRefresh = false)
        {
            return await Task.FromResult(_charDataset);
        }
        #endregion Character
    }
}