using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace enjoii.Items
{
    public class ItemDatabase : MonoBehaviour
    {
        private List<Item> items = new List<Item>();

        private void Awake()
        {
            BuildItemDatabase();
        }

        public Item GetItem(int id)
        {
            return items.Find(item => item.id == id);
        }

        public Item GetItem(string fileName)
        {
            return items.Find(item => item.fileName == fileName);

        }
        private void BuildItemDatabase()
        {
            items = new List<Item>()
        {
            new Item(1, "Magical Essence", "Stuff that drops from enemies.", "item_magical_essence",
            new Dictionary<string, int>
            {
                {"Power", 15},
                {"Defence", 7}
            }),

            new ConsumableItem(2, "Health Potion", "Heals for 5 HP.", "item_health_potion", 5,
            new Dictionary<string, int> { }
            ),

            new EquipmentItem(3, "Wooden Bow", "Just a regular wooden bow.", "item_weapon_bow", EquipmentType.Weapon,
            new Dictionary<string, int>
            {
                {"Power", 12},
                {"Defence", 1}
            }),

            new EquipmentItem(4, "Strong Bow", "A stronger bow.", "item_weapon_strong_bow", EquipmentType.Weapon,
            new Dictionary<string, int>
            {
                {"Power", 15},
                {"Defence", 1}
            }),

            new EquipmentItem(5, "Basic Armor", "Just a basic armor set.", "item_helmet_basic_armor", EquipmentType.Helmet,
            new Dictionary<string, int>
            {
                {"Power", 2},
                {"Defence", 2}
            }),
        };
        }

        public GameObject GetSpawnablePrefab(int id)
        {
            Item itemToSearch = GetItem(id);

            if (itemToSearch == null)
            {
                Debug.LogError($"Item with {id} was not found in the database.");
                return null;
            }

            string searchName = $"prefab_{itemToSearch.fileName}";
            GameObject prefab = Resources.Load<GameObject>($"Prefabs/Items/{searchName}");
            
            if(prefab == null)
            {
                Debug.Log("Could not find prefab!");
                return null;
            }

            return prefab;
        }

        public GameObject GetSpawnablePrefab(string fileName)
        {
            string searchName = $"prefab_{fileName}";
            GameObject prefab = Resources.Load<GameObject>($"Prefabs/Items/{searchName}");

            if (prefab == null)
            {
                Debug.Log("Could not find prefab!");
                return null;
            }

            return prefab;
        }
    }
}