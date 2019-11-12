using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace enjoii.Items
{
    public class ItemChest : ItemContainer
    {
        // Inspector Fields
        [SerializeField] private Transform itemsParent;
        [SerializeField] private KeyCode openKey = KeyCode.F;

        // Private Variables
        private bool isOpen;
        private bool isInRange;

        protected override void OnValidate()
        {
            if (itemsParent != null)
                itemsParent.GetComponentsInChildren(includeInactive: true, result: ItemSlots);
        }

        protected override void Awake()
        {
            base.Awake();
            itemsParent.gameObject.SetActive(false);
        }
        private void Update()
        {
            if (isInRange && Input.GetKeyDown(openKey))
            {
                isOpen = !isOpen;
                itemsParent.gameObject.SetActive(isOpen);

                if (isOpen)
                    GameManager.Instance.PlayerRef.OpenItemContainer(this);
                else
                    GameManager.Instance.PlayerRef.CloseItemContainer(this);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            CheckCollision(other.gameObject, true);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            CheckCollision(other.gameObject, false);
        }

        private void CheckCollision(GameObject gameObject, bool state)
        {
            if (gameObject.CompareTag("Player"))
            {
                isInRange = state;

                if (!isInRange && isOpen)
                {
                    isOpen = false;
                    itemsParent.gameObject.SetActive(false);
                    GameManager.Instance.PlayerRef.CloseItemContainer(this);
                }
            }
        }
    }
}

