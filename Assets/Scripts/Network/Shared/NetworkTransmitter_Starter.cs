using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The class responsible for starting a client or a server and updating them
/// </summary>
public class NetworkTransmitter_Starter : MonoBehaviour {
    Client client;
    private GUIHandler guiHandler;

    private void Start()
    {
        guiHandler = new GUIHandler();
    }

    /// <summary>
    /// Starts as the role of a client
    /// </summary>
    public void StartClient()
    {
        client = new Client(new Shared_PlayerInfo() { name = "Client" });
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
