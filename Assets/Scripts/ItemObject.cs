using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using enjoii.Items;
using enjoii.Characters;

public class ItemObject : MonoBehaviour
{
    // Inspector Fields
    [SerializeField] private string itemID;
    [SerializeField] private SpriteRenderer itemSprite;

    private Rigidbody2D rBody;

    private void Awake()
    {
        rBody = GetComponent<Rigidbody2D>();
    }

    public void SetItem(Item item)
    {
        itemID = item.fileName;
        itemSprite.sprite = item.icon;
    }

    public void MoveItemInRandomDirection()
    {
        rBody.AddForce(new Vector2(Random.Range(5, 10), Random.Range(5, 10)), ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();

            if(player != null)
            {
                if (player.PickupItem(itemID))
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
