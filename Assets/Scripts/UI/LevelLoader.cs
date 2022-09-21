using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : SceneLoaderManager
{
    private SceneLoaderManager sceneLdrMgr;
    private void Start() {
        sceneLdrMgr = GameObject.FindWithTag("SceneLoader").GetComponent<SceneLoaderManager>();
        LevelLoaderAsync(sceneLdrMgr.sceneName);
    }
    public void LevelLoaderAsync(string sceneName) {
        StartCoroutine(LevelLoadingAsync(sceneName));
    }
    private IEnumerator LevelLoadingAsync(string sceneName) {
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        if (sceneName != "MainMenu" && sceneName != "Intro")
            SceneManager.LoadSceneAsync("GameGui", LoadSceneMode.Additive);
        Destroy(sceneLdrMgr.gameObject);
        yield return null;
    }
}
