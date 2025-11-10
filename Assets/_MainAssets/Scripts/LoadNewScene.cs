using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNewScene : MonoBehaviour
{
    public void LoadScene(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }
    public void LoadSceneDelayed(string sceneToLoad)
    {
        StartCoroutine(DelayedLoad(sceneToLoad));
    }
    public IEnumerator DelayedLoad(string sceneToLoad)
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(sceneToLoad);
    }
}
