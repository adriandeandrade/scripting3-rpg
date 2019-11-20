using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using enjoii.Items;

public class Notification : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI notificationText;
    [SerializeField] private Animator notificationAnimation;
    [SerializeField] private float fadeTime = 2;

    private float currentFadeTime;

    public void SetNotificationData(Item item)
    {
        currentFadeTime = fadeTime;
        notificationText.SetText($"+1 {item.name}");
    }

    public void Update()
    {
        if(currentFadeTime > 0)
        {
            currentFadeTime -= Time.deltaTime;
        }
        else
        {
            notificationAnimation.SetTrigger("Fade");
        }
    }

    // Called from animation event
    public void OnFadeFinished()
    {
        Destroy(gameObject);
    }
}
