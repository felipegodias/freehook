using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerPrefsEditor {

    [MenuItem("PlayerPrefs/Clear")]
    public static void ClearPlayerPrefs() {
        PlayerPrefs.DeleteAll();
    }

}
