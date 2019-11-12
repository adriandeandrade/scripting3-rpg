using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using enjoii.Characters;

namespace enjoii.Items
{
    public abstract class UsableItemBuff : ScriptableObject
    {
        public abstract void ExecuteEffect(UsableItem parentItem, BaseCharacter character);
        public abstract string GetDescription();
}
}

