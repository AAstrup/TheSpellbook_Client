using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdapterMatch : MonoBehaviour {
    private Client client;

    // Use this for initialization
    void Start () {
        PersistentData data = AppConfig.GetPersistentData();
        Client_MessageHandler messageHandler = new Client_MessageHandler();
        client = new Client(new ConnectionInfo(data.port,data.ip), messageHandler);
        messageHandler.Init(client);

    }
	
	// Update is called once per frame
	void Update () {
        client.Update();
	}
}
