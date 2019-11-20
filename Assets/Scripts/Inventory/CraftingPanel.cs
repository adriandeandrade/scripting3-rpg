using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace enjoii.Items
{
    public class CraftingPanel : MonoBehaviour
    {
        // Inspector Fields
        [SerializeField] RecipeDatabase recipeDatabase;
        [SerializeField] private ItemSlot craftResultSlot;

        // Private Variables
        private List<ItemSlot> uiItems = new List<ItemSlot>();

        private void Awake()
        {
            craftResultSlot.SlotType = SlotType.CraftingResultSlot;
        }

        private void Start()
        {
            uiItems = GetComponent<SlotPanel>().ItemSlots;
            uiItems.ForEach(i => i.SlotType = SlotType.CraftingSlot);
        }

        public void UpdateRecipe()
        {
            int[] itemTable = new int[uiItems.Count];
            for (int i = 0; i < uiItems.Count; i++)
            {
                if (uiItems[i].ItemInSlot != null)
                {
                    itemTable[i] = uiItems[i].ItemInSlot.id;
                }
            }

            Item itemToCraft = recipeDatabase.CheckRecipe(itemTable);
            UpdateCraftingResultSlot(itemToCraft);
        }

        private void UpdateCraftingResultSlot(Item itemToCraft)
        {
            craftResultSlot.UpdateSlot(itemToCraft);
        }


    }
}
