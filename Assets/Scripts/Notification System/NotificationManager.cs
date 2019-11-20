using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using enjoii.Items;

public class NotificationManager : MonoBehaviour
{
    [SerializeField] private Transform notificationParent;
    [SerializeField] private GameObject notificationPrefab;

    private void Awake()
    {
        notificationParent = GameObject.Find("NotificationBox").transform;
    }

    public void SpawnNotification(Item item)
    {
        Notification newNotification = Instantiate(notificationPrefab, notificationParent).GetComponent<Notification>();
        newNotification.SetNotificationData(item);
    }
}
