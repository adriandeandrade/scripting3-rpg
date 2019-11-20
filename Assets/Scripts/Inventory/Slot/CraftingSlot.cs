using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace enjoii.Items.Slot
{
    public class CraftingSlot : BaseItemSlot
    {
        // Private Variables
        private CraftingPanel craftingPanel;

        protected override void Awake()
        {
            base.Awake();
            craftingPanel = FindObjectOfType<CraftingPanel>();
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);

            if (ItemInSlot != null)
            {
                if (ItemInSlot is EquipmentItem)
                {
                    // If we right click on the slot.
                    if (eventData != null && eventData.button == PointerEventData.InputButton.Right)
                    {
                        EquipmentItem equipmentItemClone = new EquipmentItem(ItemInSlot as EquipmentItem);
                        GameManager.Instance.PlayerRef.EquipmentManager.Equip(equipmentItemClone);

                        Debug.Log($"{this.ItemInSlot.name} has been equipped.");

                        UpdateSlot(null);

                        return;
                    }
                }

                // If we click a slot with an item and if we are holding an item in our hand.
                if (selectedItem.ItemInSlot != null)
                {
                    Item itemClone = new Item(selectedItem.ItemInSlot);

                    selectedItem.UpdateSlot(ItemInSlot); // Set the hand item to the item in the slot we clicked.
                    UpdateSlot(itemClone); // Set the item in this slot to the item in our hand.
                }
                else // If we click a slot with an item and if we are not holding an item in out hand.
                {
                    selectedItem.UpdateSlot(ItemInSlot); // Set the selected item to be the item in the slot we clicked.
                    UpdateSlot(null); // Clear the slot we clicked.
                }
            }
            else if (selectedItem.ItemInSlot != null && slotType != SlotType.CraftingResultSlot && slotType != SlotType.EquippableSlot) // If no item in this slot and item in hand.
            {
                UpdateSlot(selectedItem.ItemInSlot);
                selectedItem.UpdateSlot(null);
            }

            craftingPanel.UpdateRecipe();
        }
    }
}


