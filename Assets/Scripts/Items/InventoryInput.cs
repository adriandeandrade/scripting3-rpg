using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryInput : MonoBehaviour
{
    // Inspector Fields
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject equipmentPanel;
    [SerializeField] private KeyCode toggleInventoryKey;
    [SerializeField] private KeyCode toggleEquipmentPanelKey;

    private void Update()
    {
        if (Input.GetKeyDown(toggleInventoryKey))
        {
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
            if(inventoryPanel.activeSelf)
            {
                equipmentPanel.SetActive(true);
            }
            else
            {
                equipmentPanel.SetActive(false);
            }
        }
    }
}
