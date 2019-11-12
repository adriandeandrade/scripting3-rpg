using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using enjoii.Characters;

public class Cheats : MonoBehaviour
{
    [SerializeField] private Player player;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            if (player != null)
            {
                player.TakeDamage(5f);
            }
        }

        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            if (player != null)
            {
                player.IncreaseHealth(5f);
            }
        }

        if(Input.GetKeyDown(KeyCode.O))
        {
            player.OnXPAdded(45f);
        }
    }
}
