using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace enjoii.Items
{
    public class Tooltip : MonoBehaviour
    {
        // Inspector Fields
        [SerializeField] private TextMeshProUGUI itemNameText;
        [SerializeField] private TextMeshProUGUI itemDescriptionText;

        private void Start()
        {
            gameObject.SetActive(false);
        }

        public void GenerateToolTip(Item item)
        {
            string statText = "";

            foreach (var stat in item.stats)
            {
                statText += $"{stat.Key.ToString()} : {stat.Value}\n";
            }

            itemNameText.SetText(item.name);
            itemDescriptionText.SetText(item is ConsumableItem ? ($"Right Click to Use\n {statText}") : statText);
            gameObject.SetActive(true);
        }
    }
}
