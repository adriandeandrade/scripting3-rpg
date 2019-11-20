using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace enjoii.Items
{
    public class Inventory : MonoBehaviour
    {
        public List<Item> items = new List<Item>();

        private ItemDatabase itemDatabase;
        [SerializeField] private UIInventory inventoryUI;

        private void Start()
        {
            itemDatabase = GameManager.Instance.ItemDatabase;

            GiveItem(1);
            GiveItem(2);
            GiveItem(3);
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                GiveItem(1);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                GiveItem(2);
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                GiveItem(3);
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                GiveItem(4);
            }

            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                GiveItem(5);
            }
        }

        public void GiveItem(int id)
        {
            Item itemToAdd = itemDatabase.GetItem(id);
            inventoryUI.AddItemToUI(itemToAdd);
            items.Add(itemToAdd);
        }

        public void GiveItem(string fileName)
        {
            Item itemToAdd = itemDatabase.GetItem(fileName);
            inventoryUI.AddItemToUI(itemToAdd);
            items.Add(itemToAdd);
        }

        public Item CheckForItem(int id)
        {
            Item foundItem = items.Find(item => item.id == id);

            if(foundItem != null)
            {
                return foundItem;
            }
            else
            {
                Debug.Log($"Item with {id} was not found.");
                return null;
            }
        }

        public Item CheckForItem(string fileName)
        {
            Item foundItem = items.Find(item => item.fileName == fileName);

            if (foundItem != null)
            {
                return foundItem;
            }
            else
            {
                Debug.Log($"Item with {fileName} was not found.");
                return null;
            }
        }

        public void RemoveItem(int id)
        {
            Item itemToRemove = CheckForItem(id);
            if (itemToRemove != null)
            {
                items.Remove(itemToRemove);
            }
        }

        public void RemoveItem(string fileName)
        {
            Item itemToRemove = CheckForItem(fileName);
            if (itemToRemove != null)
            {
                items.Remove(itemToRemove);
            }
        }
    }
}
