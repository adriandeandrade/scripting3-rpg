using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using enjoii.Characters;

public class BaseEnemy : BaseCharacter
{
    // Inspector Fields
    [Header("Base Enemy Setup")]
    [SerializeField] private int xpDropAmount;
    [SerializeField] private GameObject dieParticle;

    public override void Kill()
    {
        GameManager.Instance.PlayerRef.OnXPAdded(xpDropAmount);
        GameObject dieParticleEffect = Instantiate(dieParticle, transform.position, Quaternion.identity);
        Destroy(dieParticleEffect, 2f);
        base.Kill();
    }
}
