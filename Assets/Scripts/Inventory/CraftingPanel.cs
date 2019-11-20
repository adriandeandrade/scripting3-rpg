using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace enjoii.Items
{
    public class CraftingPanel : MonoBehaviour
    {
        public CraftingDatabase recipeDatabase;
        public ItemSlot craftResultSlot;

        private List<ItemSlot> uiItems = new List<ItemSlot>();

        private void Awake()
        {
            craftResultSlot.slotType = SlotType.CraftingResultSlot;
        }

        private void Start()
        {
            uiItems = GetComponent<SlotPanel>().itemSlots;
            uiItems.ForEach(i => i.slotType = SlotType.CraftingSlot);
        }

        public void UpdateRecipe()
        {
            int[] itemTable = new int[uiItems.Count];
            for (int i = 0; i < uiItems.Count; i++)
            {
                if (uiItems[i].item != null)
                {
                    itemTable[i] = uiItems[i].item.id;
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
