using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace enjoii.Items.Slot
{
    public class TrashSlot : BaseItemSlot
    {
        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);

            if(selectedItem.ItemInSlot != null)
            {
                GameManager.Instance.PlayerRef.Inventory.RemoveItem(selectedItem.ItemInSlot.id);
                selectedItem.UpdateSlot(null);
                UpdateSlot(null);
            }
        }
    }
}

