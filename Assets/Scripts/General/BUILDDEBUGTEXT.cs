using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BUILDDEBUGTEXT : MonoBehaviour {
    public static BUILDDEBUGTEXT instance;
    void Awake()
    {
        instance = this;
        gameObject.SetActive(false);
    }
    public Text t;
    public void Log(string s)
    {

    }
    public void OnlyLog(string s)
    {
        if (!gameObject.activeSelf)
            gameObject.SetActive(true);
        t.text += "- " + s + System.Environment.NewLine;
    }
}
