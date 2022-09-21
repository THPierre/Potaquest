using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartupLoader : MonoBehaviour
{
    //Public variables
    public PlayerPreferences playerPrefs;
    public int seconds;
    //private variables
    private string sceneName;
    private float progress = 0;
    private int introSkip;
    private void Start() {
        introSkip = PlayerPrefs.GetInt("LastLevel");
        FirstSceneInit();
    }
    private void Update() {
        StartupCountDown(seconds);
    }
    private void FirstSceneInit() {
        if (introSkip != 0)
            sceneName = "MainMenu";
        else
            sceneName = "Intro";
    }
    private void StartupCountDown(int seconds) {
        progress += Time.deltaTime;
        if (progress >= seconds)
            ToLoadingScreen(sceneName);
    }
    private void ToLoadingScreen(string sceneName) {
        StartCoroutine(LevelLoadingAsync(sceneName));
    }
    private IEnumerator LevelLoadingAsync(string sceneName) {
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        yield return null;
    }
}
