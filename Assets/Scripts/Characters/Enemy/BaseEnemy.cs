using System.Collections.Generic;
using UnityEngine;

using enjoii.Characters;
using enjoii.Items;

public abstract class BaseEnemy : BaseCharacter
{
    // Inspector Fields
    [Header("Base Enemy Setup")]
    [SerializeField] private int xpDropAmount;
    [SerializeField] private List<int> lootDrops = new List<int>(); 
    [SerializeField] private GameObject dieParticle;

    // Private Variables
    protected GameObject target;

    // Events
    public event System.Action OnTargetLost;

    // Properties
    public GameObject Target
    {
        get
        {
            if (target == null) target = FindObjectOfType<Player>().gameObject;

            if (target == null)
            {
                if(OnTargetLost != null)
                {
                    OnTargetLost();
                }

                return null;
            }
            else
            {
                return target;
            }
        }
    }

    public override void Kill()
    {
        GameManager.Instance.PlayerRef.OnXPAdded(xpDropAmount);

        GameObject dieParticleEffect = Instantiate(dieParticle, transform.position, Quaternion.identity);
        Destroy(dieParticleEffect, 2f);

        DropLoot();

        base.Kill();
    }

    private void DropLoot()
    {
        if (lootDrops.Count <= 0 || lootDrops == null) return;

        int dropAmount = Random.Range(1, 5);

        for (int i = 0; i < dropAmount; i++)
        {
            int randomIndex = Random.Range(0, lootDrops.Count);

            int nextItemSpawnID = lootDrops[randomIndex];
            Item itemToDrop = GameManager.Instance.ItemDatabase.GetItem(nextItemSpawnID);

            GameObject lootItemInstace = Instantiate(GameManager.Instance.ItemDatabase.GetSpawnablePrefab(nextItemSpawnID), transform.position, Quaternion.identity);
            lootItemInstace.GetComponent<ItemObject>().MoveItemInRandomDirection();
        }
    }

    protected float GetDistanceToTarget()
    {
        return Vector2.Distance(Target.transform.position, transform.position);
    }
}
