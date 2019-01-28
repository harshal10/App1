using System;
using System.Collections.Generic;
using System.Text;

namespace App1.Models
{
    public class Character
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Age { get; set; }

        public Character()
        {
            CreateDefault();
        }

        private void CreateDefault()
        {
            Name = "unknown";
            Description = "Unknown";
            Age = 1;
        }
        public Character(string name, string description, int age)
        {
            CreateDefault();

            Name = name;
            Description = description;
            Age = age;
        }

        public Character(Character Data)
        {
            CreateDefault();
            Name = Data.Name;
            Description = Data.Description;
            Age = Data.Age;
        }

        public void Update(Character newData)
        {
            if (newData == null)
            {
                return;
            }

            Name = newData.Name;
            Description = newData.Description;
            Age = newData.Age;
        }

    }
}