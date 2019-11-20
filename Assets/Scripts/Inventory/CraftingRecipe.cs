using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace enjoii.Items
{
    public class CraftingRecipe
    {
        public int[] requiredItems;
        public int itemToCraft;

        public CraftingRecipe(int itemToCraft, int[] requiredItems)
        {
            this.requiredItems = requiredItems;
            this.itemToCraft = itemToCraft;
        }
    }
}
