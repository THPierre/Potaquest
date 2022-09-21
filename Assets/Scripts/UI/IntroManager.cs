using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class IntroManager : MonoBehaviour
{
    public SceneLoaderManager sceneLoaderMgr;
    public VideoPlayer introVid;
    public string tutorialScene;
    public void SkipIntro(string sceneToLoad) {
        sceneLoaderMgr.ToLoadingScreen(sceneToLoad);
    }
    private void Update() {
        if ((ulong)introVid.frame == introVid.frameCount - 5)
            IntroDone(tutorialScene);
    }
    public void IntroDone(string sceneToLoad) {
        sceneLoaderMgr.ToLoadingScreen(sceneToLoad);
    }
}
