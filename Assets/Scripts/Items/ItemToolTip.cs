using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


namespace enjoii.Items
{
    public class ItemToolTip : MonoBehaviour
    {
        // Inspector Fields
        [Header("Tooltip Configuration")]
        [SerializeField] private TextMeshProUGUI itemNameText = null;
        [SerializeField] private TextMeshProUGUI itemTypeText = null;
        [SerializeField] private TextMeshProUGUI itemDescriptionText = null;
        [Space]
        [SerializeField] private RectTransform rect = null;
        [SerializeField] private Canvas toolTipCanvas = null;
        [SerializeField] private float padding = 15f;
        [SerializeField] private Vector3 offset = Vector3.zero;

        // Private variables
        private bool isActive;

        private void Awake()
        {
            rect = GetComponent<RectTransform>();
            gameObject.SetActive(false);
        }

        private void Update()
        {
            FollowMouse();
        }

        private void FollowMouse()
        {
            if (!isActive) return;

            Vector3 newPos = Input.mousePosition + offset;
            newPos.z = 0f;

            // Find the distance from the right edge of the screen 
            float rightEdgeToScreenEdgeDistance = Screen.width - (newPos.x + rect.rect.width * toolTipCanvas.scaleFactor / 2) - padding;
            if (rightEdgeToScreenEdgeDistance < 0)
            {
                newPos.x += rightEdgeToScreenEdgeDistance;
            }

            float leftEdgeToScreenEdgeDistance = 0 - (newPos.x - rect.rect.width * toolTipCanvas.scaleFactor / 2) + padding;
            if (leftEdgeToScreenEdgeDistance > 0)
            {
                newPos.x += leftEdgeToScreenEdgeDistance;
            }

            float topEdgeToScreenEdgeDistance = Screen.height - (newPos.y + rect.rect.height * toolTipCanvas.scaleFactor) - padding;
            if (topEdgeToScreenEdgeDistance < 0)
            {
                newPos.y += topEdgeToScreenEdgeDistance;
            }

            rect.transform.position = newPos;
        }

        public void ShowToolTip(Item item)
        {
            itemNameText.SetText($"{item.ItemName}");
            itemTypeText.SetText($"{item.GetItemType()}");
            itemDescriptionText.SetText($"{item.GetDescription()}");
            gameObject.SetActive(true);
            isActive = true;
        }

        public void HideToolTip()
        {
            gameObject.SetActive(false);
            isActive = false;
        }
    }
}

