using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Match_DotNetAdapter : MonoBehaviour {
    public static Match_DotNetAdapter instance;

    public MatchClient clientEndPoint;
    private Match_GUIHandler mM_GUIHandler;

    void Awake()
    {
        instance = this;
        mM_GUIHandler = new Match_GUIHandler();
    }

    void Start()
    {
    }

    // Use this for initialization
    public void StartOnlineClient (ILogger logger) {
        var matchEventHandler = new Match_EventHandler(mM_GUIHandler);
        var data = UnityConfig.GetPersistentDataContainer().persistentData;
        var messageHandlerExpansion = MessageHandlerFactory.CreateMessageHandlerExpansion();
        var config = new UnityClientConfig(InGameWrapper.instance.clockWrapper);
        clientEndPoint = new MatchClient(matchEventHandler, logger, data, messageHandlerExpansion, config);
    }
	
	// Update is called once per frame
	void Update () {
        if(clientEndPoint != null)
            clientEndPoint.Update(Time.deltaTime);
	}

    public void Send(object msg)
    {
        clientEndPoint.Send(msg);
    }
}
