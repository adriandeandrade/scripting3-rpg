using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace enjoii.Items
{
    public abstract class Item : ScriptableObject
    {
        [Header("Item Data")]
        [SerializeField] private string itemName = "New Item Name";
        [SerializeField] private string itemDescription = "New Item Description";
        [SerializeField] [Range(1, 999)] private int maxStackSize = 1;
        [SerializeField] private Sprite itemIcon = null;

        private string itemID;

        public string ItemName { get => itemName; }
        public string ItemDescription { get => itemDescription; }
        public int MaxStackSize { get => maxStackSize; }
        public string ItemID => itemID;
        public Sprite ItemIcon { get => itemIcon; }

        protected static readonly StringBuilder sb = new StringBuilder();

        #if UNITY_EDITOR
        protected virtual void OnValidate()
        {
            string path = AssetDatabase.GetAssetPath(this);
            itemID = AssetDatabase.AssetPathToGUID(path);
        }
        #endif

        public virtual Item GetCopy()
        {
            return this;
        }

        public virtual void Destroy()
        {

        }

        public virtual string GetItemType()
        {
            return "";
        }

        public virtual string GetDescription()
        {
            // TODO: wadawd

            return "";
        }

        public GameObject GetSpawnablePrefab()
        {
            string searchName = this.name;
            int index1 = searchName.IndexOf("(Clone)"); // Since we are creating copies of the object, the word Clone gets added onto the end of the object name so I have to remove it.

            if (index1 != -1)
            {
                searchName = searchName.Remove(index1);
            }

            GameObject prefab = Resources.Load<GameObject>($"Prefabs/Items/prefab_{searchName}");

            if(prefab == null)
            {
                Debug.Log("Could not find prefab.");
                return null;
            }

            return prefab;
        }
    }
}
