using System;
using UnityEngine;

internal class MM_GUIHandler
{
    public static MM_GUIHandler Instance;
    private GameObject MM;
    private GameObject Connecting;
    private GameObject InQueue;

    public MM_GUIHandler()
    {
        Instance = this;
        MM = GameObject.Find("GUI_MainMenu");
        Connecting = GameObject.Find("GUI_Connecting");
        InQueue = GameObject.Find("GUI_InQueue");
        SetUIState_MM();
    }

    public void SetUIState_MM()
    {
        DisableAll();
        MM.SetActive(true);
    }

    public void SetUIState_Connecting()
    {
        DisableAll();
        Connecting.SetActive(true);
    }

    public void SetUIState_Queue(Message_Response_InQueue data)
    {
        DisableAll();
        InQueue.SetActive(true);
    }

    private void DisableAll()
    {
        MM.SetActive(false);
        Connecting.SetActive(false);
        InQueue.SetActive(false);
    }
}