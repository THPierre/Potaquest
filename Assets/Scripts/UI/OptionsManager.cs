using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionsManager : MonoBehaviour
{
    public GameObject options;
    public Animator optionsAnimator;
    public Toggle vibrations;
    public Toggle musics;
    public Preferences prefs;
    private void Start() {
        if (prefs.vibrationsPreferences == 0)
            vibrations.isOn = true;
        else
            vibrations.isOn = false;
        if (prefs.musicPreferences == 0)
            musics.isOn = true;
        else
            musics.isOn = false;
    }
    public void OpenOptions(bool enabled) {
        options.SetActive(enabled);
        optionsAnimator.SetTrigger("Transition");
        if (!enabled)
            PlayerPrefs.Save();
        if (prefs.vibrationsPreferences == 0)
            vibrations.isOn = true;
        else
            vibrations.isOn = false;
        if (prefs.musicPreferences == 0)
            musics.isOn = true;
        else
            musics.isOn = false;
    }
}
