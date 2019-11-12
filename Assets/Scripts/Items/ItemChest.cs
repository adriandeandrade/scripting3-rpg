using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace enjoii.Items
{
    public class ItemChest : ItemContainer
    {
        // Inspector Fields
        [SerializeField] private Transform itemsParent;
        [SerializeField] private Transform inventoryParent;
        [SerializeField] private Transform originalParent;
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
            ToggleChestUI(false);
        }
        private void Update()
        {
            if (isInRange && Input.GetKeyDown(openKey))
            {
                isOpen = !isOpen;
                ToggleChestUI(isOpen);

                if (isOpen)
                    GameManager.Instance.PlayerRef.OpenItemContainer(this);
                else
                    GameManager.Instance.PlayerRef.CloseItemContainer(this);
            }
        }

        public void ToggleChestUI(bool state)
        {
            if(state == true)
            {
                itemsParent.parent = inventoryParent;
            }
            else if(state == false)
            {
                itemsParent.parent = originalParent;
            }

            itemsParent.gameObject.SetActive(state);
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
                    ToggleChestUI(state);
                    GameManager.Instance.PlayerRef.CloseItemContainer(this);
                }
            }
        }
    }
}

