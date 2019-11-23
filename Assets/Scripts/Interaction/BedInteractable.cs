using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedInteractable : Interactable
{
    public override void Interact()
    {
        Debug.Log("Bed used.");
    }
}
