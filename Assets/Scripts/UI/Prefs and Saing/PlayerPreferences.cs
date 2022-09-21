using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPreferences : MonoBehaviour
{
    [HideInInspector]
    public string sceneName;
    private void Start() {
        sceneName = PlayerPrefs.GetInt("LastLevel").ToString();
    }
    public void Resetlastlevel() {
        PlayerPrefs.DeleteKey("LastLevel");
    }
    public void SetLastLevel(int lastLevel) {
        PlayerPrefs.SetInt("LastLevel", lastLevel);
    }
}
