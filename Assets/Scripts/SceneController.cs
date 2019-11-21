using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void LoadLevelAsync()
    {
        var operation = SceneManager.LoadSceneAsync("Game");
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
