using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public SceneLoaderManager sceneLoaderMgr;
    public PlayerPreferences playerPrefs;
    public GameObject creditsScreen;
    private string sceneToLoad;
    public AudioSource btnSound;
    public void OpenCredits(bool enabled) {
        creditsScreen.SetActive(enabled);
    }
    public void PlaySound() {
        btnSound.Play();
    }
    public void SelectSceneToLoad(string sceneToLoad) {
        if (sceneToLoad == "Resume")
            this.sceneToLoad = playerPrefs.sceneName;
        else
            this.sceneToLoad = sceneToLoad;
        sceneLoaderMgr.ToLoadingScreen(this.sceneToLoad);
    }
}
