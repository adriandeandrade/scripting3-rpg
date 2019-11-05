using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Items/New Item")]
public class Item : ScriptableObject
{
    [Header("Item Data")]
    public string itemName = "New Item Name";
    public string itemDescription = "New Item Description";
    public int maxStackAmount = 1;
    public Sprite itemIcon = null;
}
