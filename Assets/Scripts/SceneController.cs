using enjoii.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode loadMode)
    {
        if(scene.name == "Game")
        {
            GameManager.Instance.PlayerRef.EquipmentManager = FindObjectOfType<EquipmentManager>();
            GameManager.Instance.Load();
        }
    }

    public void LoadLevelAsync(string sceneName)
    {
        var operation = SceneManager.LoadSceneAsync(sceneName);
        Debug.Log("Starting load....");

        operation.allowSceneActivation = false;

        StartCoroutine(WaitForLoadup(operation));
    }
    private IEnumerator WaitForLoadup(AsyncOperation operation)
    {
        while(operation.progress < 0.9f)
        {
            yield return null;
        }

        Debug.Log("Loading Complete");

        operation.allowSceneActivation = true;

        GameManager.Instance.Initialize();
    }
}
