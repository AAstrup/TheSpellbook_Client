using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdapterMatch : MonoBehaviour {
    private Client client;
    InGame_GUIHandler guiHandler;
    // Use this for initialization
    void Start () {
        guiHandler = new InGame_GUIHandler();
        PersistentData data = AppConfig.GetPersistentData();
        MatchMessageHandler messageHandler = new MatchMessageHandler();
        client = new Client(new ConnectionInfo(data.port,data.ip), messageHandler);
        Message_Request_JoinGame request = new Message_Request_JoinGame()
        {
            info = AppConfig.GetPersistentData().PlayerInfo
        };
        client.sender.Send(request);
    }
	
	// Update is called once per frame
	void Update () {
        client.Update();
	}
}
