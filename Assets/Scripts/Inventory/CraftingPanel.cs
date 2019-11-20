using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using enjoii.Items.Slot;

namespace enjoii.Items
{
    public class CraftingPanel : MonoBehaviour
    {
        // Inspector Fields
        [SerializeField] RecipeDatabase recipeDatabase;
        [SerializeField] private BaseItemSlot craftResultSlot;

        // Private Variables
        private List<BaseItemSlot> craftingSlots = new List<BaseItemSlot>();

        private void Awake()
        {
            craftResultSlot.SlotType = SlotType.CraftingResultSlot;
        }

        private void Start()
        {
            craftingSlots = GetComponent<SlotPanel>().ItemSlots; // Get the slots from the panel.
            craftingSlots.ForEach(i => i.SlotType = SlotType.CraftingSlot); // Set every slot in the crafting panel to crafting slots.
        }

        public void UpdateRecipe()
        {
            int[] itemTable = new int[craftingSlots.Count]; // Update items in crafting slots. [ 0 = No item in slot, anything else is an item id]
            for (int i = 0; i < craftingSlots.Count; i++)
            {
                if (craftingSlots[i].ItemInSlot != null)
                {
                    itemTable[i] = craftingSlots[i].ItemInSlot.id; // Loop through the item table and see what item is in the current crafting slot and set its id.
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
