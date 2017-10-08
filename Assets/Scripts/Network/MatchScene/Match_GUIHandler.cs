using System;
using UnityEngine;

internal class Match_GUIHandler
{
    public static Match_GUIHandler Instance;
    public GameObject WonPanel;
    public GameObject LostPanel;

    public Match_GUIHandler()
    {
        Instance = this;
        WonPanel = GameObject.Find("WonPanel");
        LostPanel = GameObject.Find("LostPanel");
    }

    internal void SetState_Default()
    {
        CloseAllWindows();
    }

    void CloseAllWindows()
    {
        WonPanel.SetActive(false);
        LostPanel.SetActive(false);
    }

    public void SetState_Win()
    {
        CloseAllWindows();
        WonPanel.SetActive(true);
    }

    public void SetState_Loss()
    {
        CloseAllWindows();
        LostPanel.SetActive(true);
    }
}