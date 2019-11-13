using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using enjoii.Items;

public class NotificationManager : MonoBehaviour
{
    [SerializeField] private Transform notificationParent;
    [SerializeField] private GameObject notificationPrefab;
    [SerializeField] private Item testItem;

    private void Awake()
    {
        notificationParent = GameObject.Find("NotificationBox").transform;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Y))
        {
            SpawnNotification(testItem);
        }
    }

    public void SpawnNotification(Item item)
    {
        Notification newNotification = Instantiate(notificationPrefab, notificationParent).GetComponent<Notification>();
        newNotification.SetNotificationData(item);
    }
}
