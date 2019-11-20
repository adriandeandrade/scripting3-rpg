using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleActiveWithKey : MonoBehaviour
{
    // Inspector Fields
    [SerializeField] private KeyCode toggleKey = KeyCode.None;
    [SerializeField] private GameObject objectToToggle;

    private void Update()
    {
        if(objectToToggle != null)
        {
            if(Input.GetKeyDown(toggleKey))
            {
                objectToToggle.SetActive(!objectToToggle.activeSelf);
            }
        }
    }
}
