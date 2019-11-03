using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Items/New Item")]
public class Item : ScriptableObject
{
    public new string name = "New Item";
    [TextArea] public string description;
    public Sprite icon = null; 
}
