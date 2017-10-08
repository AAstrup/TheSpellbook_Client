using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Match_DotNetAdapter : MonoBehaviour {

    private MatchClient clientEndPoint;
    private Match_GUIHandler mM_GUIHandler;

    private void Start()
    {
        mM_GUIHandler = new Match_GUIHandler();
        StartOnlineClient();
    }

    // Use this for initialization
    public void StartOnlineClient () {
        var matchEventHandler = new Match_EventHandler(mM_GUIHandler);
        var logger = new UnityLogger();
        var data = UnityConfig.GetPersistentDataContainer().persistentData;
        var messageHandlerExpansion = MessageHandlerFactory.CreateMessageHandlerExpansion();
        clientEndPoint = new MatchClient(matchEventHandler, logger, data, messageHandlerExpansion);
    }
	
	// Update is called once per frame
	void Update () {
        if(clientEndPoint != null)
            clientEndPoint.Update(Time.deltaTime);
	}
}
