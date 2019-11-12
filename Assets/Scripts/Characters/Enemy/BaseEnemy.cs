using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using enjoii.Characters;

public class BaseEnemy : BaseCharacter
{
    // Inspector Fields
    [Header("Base Enemy Setup")]
    [SerializeField] private int xpDropAmount;

    public override void Kill()
    {
        GameManager.Instance.PlayerRef.OnXPAdded(xpDropAmount);

        base.Kill();
    }
}
