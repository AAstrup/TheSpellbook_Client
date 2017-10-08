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
        IMatchEventHandler matchEventHandler = new Match_EventHandler(mM_GUIHandler);
        ILogger logger = new UnityLogger();
        PersistentData data = UnityConfig.GetPersistentDataContainer().persistentData;
        Dictionary<Type, IMessageHandlerCommandClient> dictionary = MessageHandlerFactory.CreateDictionary();
        clientEndPoint = new MatchClient(matchEventHandler, logger, data, dictionary);
    }
	
	// Update is called once per frame
	void Update () {
        if(clientEndPoint != null)
            clientEndPoint.Update(Time.deltaTime);
	}
}
