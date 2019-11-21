using enjoii.Characters;
using enjoii.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneFinishedLoading;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneFinishedLoading;
    }

    public void SaveGame()
    {
        SaveLoad.Save();
    }

    public void ExitGame()
    {
        SaveGame();
        SceneManager.LoadScene("MainMenu");
    }

    private void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        SaveLoad.Load();
    }
}
