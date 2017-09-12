using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The class responsible for starting a client or a server and updating them
/// </summary>
public class MM_NetworkTransmitter : MonoBehaviour {
    Client client;
    private MM_GUIHandler guiHandler;

    private void Start()
    {
        guiHandler = new MM_GUIHandler();
    }

    /// <summary>
    /// Starts as the role of a client
    /// </summary>
    public void StartClient()
    {   
        AppConfig.GetPersistentData().PlayerInfo = new Shared_PlayerInfo() { name = AppConfig.GetName() };
        MMMessageHandler messageHandler = new MMMessageHandler();
        client = new Client( ConnectionInfo.MatchMakerConnectionInfo(), messageHandler);
        messageHandler.Init(client);
        client.Register();
        guiHandler.SetUIState_Connecting();
    }

    /// <summary>
    /// Update the target role
    /// </summary>
    void Update()
    {
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
}
