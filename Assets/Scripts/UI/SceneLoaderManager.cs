using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoaderManager : MonoBehaviour
{
    [HideInInspector]
    public string sceneName;
    public void ToLoadingScreen(string sceneToLoad) {
        PlayerPrefs.Save();
        if(sceneToLoad == "RestartScene")
            sceneToLoad = SceneManager.GetActiveScene().name;
        sceneName = sceneToLoad;
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene("LoadingScreen");
    }
}
