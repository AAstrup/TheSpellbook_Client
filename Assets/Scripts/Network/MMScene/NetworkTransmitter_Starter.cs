using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The class responsible for starting a client or a server and updating them
/// </summary>
public class NetworkTransmitter_Starter : MonoBehaviour {
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
        MatchMessageHandler messageHandler = new MatchMessageHandler();
        client = new Client( ConnectionInfo.MatchMakerConnectionInfo(), MatchMessageHandler);
    }

    /// <summary>
    /// Update the target role
    /// </summary>
    void Update()
    {
        if (client != null)
            client.Update();
    }
}
