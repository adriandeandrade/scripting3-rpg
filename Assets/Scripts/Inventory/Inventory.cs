using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton

    public static Inventory instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("More than one instance of inventory found.");
            return;
        }

        instance = this;
    }

    #endregion

    public List<Item> items = new List<Item>();

    // Inspector Fields
    [SerializeField] private Item testItem; // Testing

    // Private Variables
    private const int slotAmount = 12;

    // Events
    public System.Action InventoryChanged;

    private void Start()
    {
        
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            AddItem(testItem, 1);
        }
    }

    public void AddItem(Item itemToAdd, int amountToAdd)
    {
        
    }

    public void RemoveItem(Item itemToRemove, int amountToRemove)
    {
        
    }

    public int GetCurrentAmount(Item itemToCheck)
    {
        return 0;
    }

    public bool CheckIfItemExists(Item itemToCheck)
    {
        return false;
    }
}
