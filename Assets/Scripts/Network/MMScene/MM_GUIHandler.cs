using System;
using UnityEngine;

internal class MM_GUIHandler
{
    public static MM_GUIHandler Instance;
    private GameObject MM;
    private GameObject InQueue;

    public MM_GUIHandler()
    {
        Instance = this;
        MM = GameObject.Find("GUI_MainMenu");
        InQueue = GameObject.Find("GUI_InQueue");
        SetUIState_MM();
    }

    private void SetUIState_MM()
    {
        DisableAll();
        MM.SetActive(true);
    }

    internal void SetUIState_Queue(Message_Response_InQueue data)
    {
        DisableAll();
        InQueue.SetActive(true);
    }

    private void DisableAll()
    {
        MM.SetActive(false);
        InQueue.SetActive(false);
    }
}