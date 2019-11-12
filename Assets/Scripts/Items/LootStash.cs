using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace enjoii.Items
{
    public class LootStash : MonoBehaviour
    {
        // Inspector Fields
        [Header("Loot Stash Configuration")]
        [SerializeField] private List<Item> possibleItems;
        [SerializeField] private int amount = 1;
        [SerializeField] private Inventory inventory;
        [SerializeField] private SpriteRenderer interactSprite;
        [SerializeField] private KeyCode lootKey = KeyCode.F;

        // Private Variables
        private bool isInRange;
        private bool isLooted;
        private List<Item> itemsInStash = new List<Item>();

        private void OnValidate()
        {
            if (inventory == null)
            {
                inventory = GameManager.Instance.PlayerRef.Inventory;
            }
        }

        private void Start()
        {
            SetRandomItems();
        }

        private void Update()
        {
            if (isInRange && !isLooted && Input.GetKeyDown(lootKey))
            {
                foreach(Item item in itemsInStash)
                {
                    Item itemCopy = item.GetCopy();

                    if (inventory.AddItem(itemCopy))
                    {
                        itemsInStash.Remove(item);
                    }
                    else
                    {
                        itemCopy.Destroy();
                    }
                }

                if(itemsInStash.Count <= 0)
                {
                    isLooted = true;
                }
            }
        }

        private void SetRandomItems()
        {
            for (int i = 0; i < amount; i++)
            {
                itemsInStash.Add(possibleItems[Random.Range(0, possibleItems.Count)]);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.CompareTag("Player"))
            {
                isInRange = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                isInRange = false;
            }
        }


    }
}


