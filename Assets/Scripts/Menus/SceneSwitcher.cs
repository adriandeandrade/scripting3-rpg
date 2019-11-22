using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(int sceneIndex)
    {
        StartCoroutine(LoadAsyncScene(sceneIndex));
    }

    private IEnumerator LoadAsyncScene(int sceneIndex)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);

        while(!asyncLoad.isDone)
        {
            yield return null;
        }

        Debug.Log("Scene Finished loading now destroying.");
        GameManager.Instance.Initialize();
        Destroy(gameObject);
    }
}
