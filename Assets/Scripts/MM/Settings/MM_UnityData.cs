using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MM_UnityData : MonoBehaviour {
    [HideInInspector]
    public static MM_UnityData instance;
    public SettingsData settingsData;

    private void Awake()
    {
        instance = this;
    }

}
