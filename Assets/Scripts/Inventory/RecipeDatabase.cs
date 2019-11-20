using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace enjoii.Items
{
    [RequireComponent(typeof(ItemDatabase))]
    public class RecipeDatabase : MonoBehaviour
    {
        private List<CraftingRecipe> recipes = new List<CraftingRecipe>();
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
                if (craftingRecipe.requiredItems.OrderBy(i => i).SequenceEqual(recipe.OrderBy(i => i))) // Order elements in the recipe so recipe is shapeless.
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
            new CraftingRecipe(3, // Crafts regular bow.
                new int[]
                {
                    0, 0, 0,
                    0, 0, 0,
                    1, 2, 0
                }),

            new CraftingRecipe(4, // Crafts strong bow.
                new int[]
                {
                    0, 1, 0,
                    0, 1, 0,
                    0, 3, 0
                }),
        };
        }
    }
}
