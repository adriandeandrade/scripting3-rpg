using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace enjoii.Items
{
    [System.Serializable]
    public class Item
    {
        public int id;
        public string name;
        public string description;
        public string fileName;
        public Sprite icon;
        public Dictionary<string, int> stats = new Dictionary<string, int>();
        

        public Item(int id, string name, string description, string fileName,Dictionary<string, int> stats)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.fileName = fileName;
            this.icon = Resources.Load<Sprite>($"Items/{fileName}");
            this.stats = stats;
        }

        public Item(Item item)
        {
            this.id = item.id;
            this.name = item.name;
            this.description = item.description;
            this.fileName = item.fileName;
            this.icon = item.icon;
            this.stats = item.stats;
        }
    }
}
