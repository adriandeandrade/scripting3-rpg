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
    [SerializeField] private float hideHealthTime = 1.5f;

    // Private Variables
    private bool showHealth = false;
    protected GameObject target;
    private float currentShowHealthTime;

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

    protected override void Start()
    {
        base.Start();
        DisableHealthbar();
    }

    protected virtual void Update()
    {
        if(currentShowHealthTime > 0)
        {
            currentShowHealthTime -= Time.deltaTime;
            return;
        }
        else
        {
            DisableHealthbar();
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

            //GameObject lootItemInstace = Instantiate(GameManager.Instance.ItemDatabase.GetSpawnablePrefab(nextItemSpawnID), transform.position, Quaternion.identity);

            ItemObject lootItemInstance = Instantiate(Resources.Load<GameObject>("Prefabs/Items/prefab_item_base"), transform.position, Quaternion.identity).GetComponent<ItemObject>();
            lootItemInstance.SetItem(itemToDrop);
            lootItemInstance.GetComponent<ItemObject>().MoveItemInRandomDirection();
        }
    }

    private void ShowHealth()
    {
        showHealth = true;
        currentShowHealthTime = hideHealthTime;

        EnableHealthBar();
    }

    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);
        ShowHealth();
    }

    protected float GetDistanceToTarget()
    {
        return Vector2.Distance(Target.transform.position, transform.position);
    }
}
