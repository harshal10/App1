using System;

namespace App1.Models
{
    public class Item
    {
        public string Id { get; set; }
        public string Guid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        //Adding image URI property 
        public string ImageURI { get; set; }

        public static string DefaultImageURI = "item.png";

        public Item()
        {
            CreateDefault();
        }

        private void CreateDefault()
        {
            Guid = System.Guid.NewGuid().ToString();
            Id = Guid;
            Name = "unknown";
            Description = "Unknown";
            ImageURI = DefaultImageURI;
        }
        public Item(string name, string description, string imageuri)
        {
            CreateDefault();
 
            Name = name;
            Description = description;
            ImageURI = imageuri;
        }
    }
}