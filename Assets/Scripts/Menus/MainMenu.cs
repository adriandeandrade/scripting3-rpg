using enjoii.Characters;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject sceneSwitcherPrefab;

    private void Awake()
    {
        SaveSystem.Init();
    }

    public void Load()
    {
        SceneSwitcher switcher = Instantiate(sceneSwitcherPrefab).GetComponent<SceneSwitcher>();
        switcher.LoadScene(1);
    }
}
