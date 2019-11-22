
using enjoii.Characters;
using System.Collections.Generic;
using UnityEngine;

namespace enjoii.Items
{
    public class ConsumableItem : Item, IConsumable
    {
        public int benefitAmount;

        

        public ConsumableItem(int id, string name, string description, string fileName, int benefitAmount, Dictionary<string, int> stats) : base(id, name, description, fileName, stats)
        {
            this.benefitAmount = benefitAmount;
        }
        public ConsumableItem(ConsumableItem consumableItem) : base(consumableItem)
        {
            this.benefitAmount = consumableItem.benefitAmount;
        }

        public void Use(Player player, ConsumableItem consumable)
        {
            player.HandleConsumable(consumable);
        }

    }
}

