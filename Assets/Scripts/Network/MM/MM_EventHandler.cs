using System;
using UnityEngine;
using UnityEngine.SceneManagement;

internal class MM_EventHandler : IMMEventHandler
{
    private MM_GUIHandler guiHandler;

    public MM_EventHandler()
    {
        guiHandler = MM_GUIHandler.Instance;
    }

    public void ConnectingAttempt(int connectionAttempts)
    {
        Debug.Log("ConnectingAttempt " + connectionAttempts);
        guiHandler.SetUIState_ConnectingAttempt(connectionAttempts);
    }

    public void ConnectingFailed()
    {
        Debug.Log("CONNECTIONFAILED");
        guiHandler.SetUIState_MM();
    }

    public void ConnectingSuccesful()
    {
        //Handled when recieveing inqueue data
    }

    public void InQueue(Message_Response_InQueue data)
    {
        guiHandler.SetUIState_Queue(data);
    }

    public void MatchFound()
    {
        SceneManager.LoadScene(UnityConfig.InGameSceneName);
    }

    public void QueueReady(Counter counter)
    {
        guiHandler.SetUIState_QueueReady();
        guiHandler.SetUISlider(counter);
    }

    public void StartedConnecting()
    {
        guiHandler.SetUIState_Connecting();
    }

    public void StartMenu()
    {
        guiHandler.SetUIState_MM();
    }
}