using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Preferences", menuName = "ScriptableObjects/Preferences", order = 1)]
public class Preferences : ScriptableObject
{
    private void Start()
    {
        musicPreferences = PlayerPrefs.GetInt("Music");
        vibrationsPreferences = PlayerPrefs.GetInt("Vibrations");
    }
    public int musicPreferences;
    public int vibrationsPreferences;
}
