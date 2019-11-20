using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace enjoii.Items
{
    [RequireComponent(typeof(ItemDatabase))]
    public class CraftingDatabase : MonoBehaviour
    {
        public List<CraftingRecipe> recipes = new List<CraftingRecipe>();
        private ItemDatabase itemDatabase;

        private void Awake()
        {
            itemDatabase = GetComponent<ItemDatabase>();
            BuildDatabase();
        }

        public Item CheckRecipe(int[] recipe)
        {
            foreach (CraftingRecipe craftingRecipe in recipes)
            {
                if (craftingRecipe.requiredItems.OrderBy(i => i).SequenceEqual(recipe.OrderBy(i => i)))
                {
                    return itemDatabase.GetItem(craftingRecipe.itemToCraft);
                }
            }

            return null;
        }

        private void BuildDatabase()
        {
            recipes = new List<CraftingRecipe>()
        {
            new CraftingRecipe(3,
                new int[]
                {
                    0, 0, 0,
                    0, 0, 0,
                    1, 2, 0
                })
        };
        }
    }
}
