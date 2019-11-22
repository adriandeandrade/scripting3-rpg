using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace enjoii.Items.Slot
{
    public class ItemSlot : BaseItemSlot
    {
        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);

            if (ItemInSlot != null)
            {
                if (eventData != null && eventData.button == PointerEventData.InputButton.Right)
                {
                    OnRightClick();
                    return;
                }
                else if (eventData != null && eventData.button == PointerEventData.InputButton.Left)
                {
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
            }
            else if (selectedItem.ItemInSlot != null && slotType != SlotType.CraftingResultSlot && slotType != SlotType.EquippableSlot) // If no item in this slot and item in hand.
            {
                UpdateSlot(selectedItem.ItemInSlot);
                selectedItem.UpdateSlot(null);
            }
        }

        private void OnRightClick()
        {
            if (ItemInSlot != null)
            {
                if (ItemInSlot is EquipmentItem)
                {
                    EquipmentItem equipmentItemClone = new EquipmentItem(ItemInSlot as EquipmentItem);

                    if (equipmentItemClone != null)
                    {
                        GameManager.Instance.PlayerRef.EquipmentManager.Equip(equipmentItemClone);

                        Debug.Log($"{this.ItemInSlot.name} has been equipped.");

                        UpdateSlot(null);

                        return;
                    }
                }
                else if (ItemInSlot is ConsumableItem)
                {
                    ConsumableItem consumableItem = new ConsumableItem(ItemInSlot as ConsumableItem);

                    if (consumableItem != null)
                    {
                        ConsumableItem consumable = ItemInSlot as ConsumableItem;
                        consumable.Use(GameManager.Instance.PlayerRef, consumableItem);

                        GameManager.Instance.PlayerRef.Inventory.Items.Remove(consumable);

                        UpdateSlot(null);

                        return;
                    }
                }
            }
        }
    }
}
