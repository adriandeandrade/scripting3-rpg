using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using enjoii.Characters;

namespace enjoii.Items
{
    [CreateAssetMenu(fileName = "New Heal Buff", menuName = "Item Buffs/New Heal Buff ")]
    public class HealBuff : UsableItemBuff
    {
        public int healAmount;
        public override void ExecuteEffect(UsableItem parentItem, BaseCharacter character)
        {
            character.IncreaseHealth(healAmount);
        }
        public override string GetDescription()
        {
            return $"Heals character for {healAmount} health.";
        }
    }
}
