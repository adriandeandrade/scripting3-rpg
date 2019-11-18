using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace enjoii.Items
{
    public class Inventory : ItemContainer
    {
        // Inspector Fields
        [Header("Inventory Configuration")]
        [SerializeField] protected Item[] startingItems;
        [SerializeField] protected Transform itemsParent;

        protected override void OnValidate()
        {
            if(itemsParent != null)
            {
                itemsParent.GetComponentsInChildren(includeInactive: true, result: ItemSlots);
            }

            if(!Application.isPlaying)
            {
                SetStartingItems();
            }
        }

        protected override void Awake()
        {
            base.Awake();
            //SetStartingItems();
        }

        private void SetStartingItems()
        {
            ClearContainer();

            foreach (Item item in startingItems)
            {
                if(item != null)
                {
                    AddItem(item.GetCopy());
                }
            }
        }
    }
}
