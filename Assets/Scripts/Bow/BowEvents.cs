using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BowEvents : MonoBehaviour
{
    // Events
    public event Action OnBowReady;
    public event Action OnBowDraw;


    // Called in animation event
    public void BowReady()
    {
        OnBowDraw();
    }

    // Called in animation event
    public void ResetBow()
    {
        OnBowReady();
    }
}
