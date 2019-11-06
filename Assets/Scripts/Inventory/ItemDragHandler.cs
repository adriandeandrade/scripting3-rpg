using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] protected InventorySlot inventorySlot = null;
    [SerializeField] private GameObject itemHolder;
    private Transform originalParent = null;
    private CanvasGroup canvasGroup;

    public InventorySlot InventorySlot => inventorySlot;

    private void Start()
    {
        itemHolder = GameObject.Find("ItemHolder");
        canvasGroup = GetComponent<CanvasGroup>();
        inventorySlot = GetComponentInParent<InventorySlot>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            originalParent = transform.parent;
            transform.SetParent(itemHolder.transform);
            canvasGroup.blocksRaycasts = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            transform.position = Input.mousePosition;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            transform.SetParent(originalParent);
            transform.localPosition = Vector3.zero;
            canvasGroup.blocksRaycasts = true;
        }
    }
}
