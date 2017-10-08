using UnityEngine.SceneManagement;

internal class MM_EventHandler : IMMEventHandler
{
    private MM_GUIHandler guiHandler;

    public MM_EventHandler()
    {
        guiHandler = MM_GUIHandler.Instance;
    }

    public void Connecting()
    {
        guiHandler.SetUIState_Connecting();
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
    }

    public void StartMenu()
    {
        guiHandler.SetUIState_MM();
    }
}