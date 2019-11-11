using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using enjoii.Characters;

namespace enjoii.Items
{
    [CreateAssetMenu(fileName = "New Usable Item", menuName = "Items/ New Usable Item")]
    public class UsableItem : Item
    {
        public bool IsConsumable;

        public List<UsableItemBuff> buffs;

        public virtual void Use(Player character)
        {
            foreach (UsableItemBuff buff in buffs)
            {
                buff.ExecuteEffect(this, character);
            }
        }

        public override string GetItemType()
        {
            return IsConsumable ? "Consumable" : "Usable";
        }

        public override string GetDescription()
        {
            sb.Length = 0;

            foreach (UsableItemBuff buff in buffs)
            {
                sb.AppendLine(buff.GetDescription());
            }

            return sb.ToString();
        }
    }
}
