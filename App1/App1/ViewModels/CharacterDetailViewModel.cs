using App1.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace App1.ViewModels
{
    public class CharacterDetailViewModel : BaseViewModel
    {
        public Character Data { get; set; }
        public CharacterDetailViewModel(Character data = null)
        {
            Title = data?.Name;
            Data = data;
        }
    }
}
