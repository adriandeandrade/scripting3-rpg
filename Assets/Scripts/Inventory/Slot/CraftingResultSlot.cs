using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace enjoii.Items.Slot
{
    public class CraftingResultSlot : BaseItemSlot
    {
        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);

            UICraftResult craftResult = GetComponent<UICraftResult>();

            if(craftResult != null && ItemInSlot != null && selectedItem.ItemInSlot == null)
            {
                //UpdateSlot(null);
                craftResult.PickItem();
                selectedItem.UpdateSlot(ItemInSlot);
                UpdateSlot(null);
                craftResult.ClearSlots();
            }
        }
    }
}


