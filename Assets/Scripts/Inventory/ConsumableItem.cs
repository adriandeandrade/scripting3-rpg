using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable Item", menuName = "Items/New Consumable Item")]
public class ConsumableItem : Item
{
    [SerializeField] private string useText = "New Consumable Use Text";

    public string UseText => useText;
}
