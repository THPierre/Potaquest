using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer musicMixer;
    public AudioSource levelMusic;
    public Preferences prefs;
    private void Start() {
        if (prefs.musicPreferences == 1)
            levelMusic.enabled = false;
        else if (prefs.musicPreferences == 0)
            levelMusic.enabled = true;
    }
    public void EnableMusic(bool enabled) {
        if (enabled) {
            levelMusic.enabled = true;
            PlayerPrefs.SetInt("Music", 0);
            prefs.musicPreferences = 0;
        } else {
            levelMusic.enabled = false;
            PlayerPrefs.SetInt("Music", 1);
            prefs.musicPreferences = 1;
        }
    }
    public void EnableVibrations(bool enabled) {
        if (enabled) {
            PlayerPrefs.SetInt("Vibrations", 0);
            prefs.vibrationsPreferences = 0;
        } else {
            PlayerPrefs.SetInt("Vibrations", 1);
            prefs.vibrationsPreferences = 1;
        }
    }
}
