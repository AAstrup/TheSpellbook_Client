using System;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// The class responsible for starting a client or a server and updating them
/// </summary>
public class MM_NetworkTransmitter : MonoBehaviour, IUnityComponentResetable
{
    Client client;
    private MM_GUIHandler guiHandler;
    private UpdateController updateController;

    private void Start()
    {
        guiHandler = new MM_GUIHandler();
        updateController = new UpdateController();
    }

    /// <summary>
    /// Starts as the role of a client
    /// </summary>
    public void StartOnlineClient()
    {
        AppConfig.GetPersistentData().PlayerInfo = new Shared_PlayerInfo() { name = AppConfig.GetName() };
        MMMessageHandler messageHandler = new MMMessageHandler(updateController);
        client = new Client(this, ConnectionInfo.MatchMakerConnectionInfo(), messageHandler);
        messageHandler.Init(client);
        client.Register();
        guiHandler.SetUIState_Connecting();
    }

    /// <summary>
    /// Starts as the role of a client
    /// </summary>
    public void StartOfflineClient()
    {
        AppConfig.GetPersistentData().PlayerInfo = new Shared_PlayerInfo() { name = AppConfig.GetName() };
        SceneManager.LoadScene(AppConfig.OfflineSceneName);
    }

    /// <summary>
    /// Update the target role
    /// </summary>
    void Update()
    {
        updateController.Update(Time.deltaTime);
        if (client != null)
            client.Update();
    }

    public void LeaveQueue()
    {
        Message_Request_LeaveQueue msg = new Message_Request_LeaveQueue()
        {
            info = AppConfig.GetPersistentData().PlayerInfo
        };
        client.sender.Send(msg);
    }

    public void Clean()
    {
        client = null;
    }

    public void ReadyForQueue()
    {
        Message_ClientResponse_ReadyCheck msg = new Message_ClientResponse_ReadyCheck()
        {
            readyCheckGUID_FromServerReadyCheck = MM_GUIHandler.queueReadyMsg.ReadGUID()
        };  
        client.sender.Send(msg);
        Debug.Log("Sending " + msg.GetType());
    }
}
