using enjoii.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Inspector Fields
    [SerializeField] private SaveManager itemSaveManager;

    private void Start()
    {
        itemSaveManager = FindObjectOfType<SaveManager>();
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }
}
