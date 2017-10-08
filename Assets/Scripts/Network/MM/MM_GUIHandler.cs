using System;
using UnityEngine;
using UnityEngine.UI;

internal class MM_GUIHandler
{
    public static MM_GUIHandler Instance;
    private GameObject MM;
    private GameObject Connecting;
    private GameObject InQueue;
    private GameObject QueueReady;
    public static Message_ServerRequest_ReadyCheck queueReadyMsg;

    public MM_GUIHandler()
    {
        Instance = this;
        MM = GameObject.Find("GUI_MainMenu");
        Connecting = GameObject.Find("GUI_Connecting");
        InQueue = GameObject.Find("GUI_InQueue");
        QueueReady = GameObject.Find("GUI_QueueReadyCheck");
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

    internal void SetUIState_QueueReady()
    {
        DisableAll();
        QueueReady.SetActive(true);
        GameObject.Find("AcceptQueueButton").GetComponent<Button>().interactable = true;
    }

    private void DisableAll()
    {
        MM.SetActive(false);
        Connecting.SetActive(false);
        InQueue.SetActive(false);
        QueueReady.SetActive(false);
    }
}