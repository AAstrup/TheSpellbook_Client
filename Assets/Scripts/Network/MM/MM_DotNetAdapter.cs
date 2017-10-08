using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MM_DotNetAdapter : MonoBehaviour {

    private MatchMakerClient clientEndPoint;
    private MM_GUIHandler mM_GUIHandler;

    private void Start()
    {
        mM_GUIHandler = new MM_GUIHandler();
    }

    // Use this for initialization
    public void StartOnlineClient () {
        IClientConfig config = new UnityClientConfig();
        IMMEventHandler iMMEventHandler = new MM_EventHandler();
        ILogger logger = new UnityLogger();
        PersistentData data = UnityConfig.GetPersistentDataContainer().persistentData;
        string name = UnityConfig.GetName();
        clientEndPoint = new MatchMakerClient(config,iMMEventHandler, logger, data, name);
    }
	
	// Update is called once per frame
	void Update () {
        if(clientEndPoint != null)
            clientEndPoint.Update(Time.deltaTime);
	}

    public void LeaveQueue()
    {
        clientEndPoint.LeaveQueue();
    }

    public void PressReady()
    {
        clientEndPoint.FinishForReadyQueue();
    }
}
