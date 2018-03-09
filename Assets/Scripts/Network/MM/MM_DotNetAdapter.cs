using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClientDB;

public class MM_DotNetAdapter : MonoBehaviour {
    public static MM_DotNetAdapter instance;

    private MatchMakerClient clientEndPoint;
    private IClientConfig config;
    MM_GUIHandler mM_GUIHandler;
    private DBClient dbClient;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        config = new UnityClientConfig(new ClockWrapper());
        mM_GUIHandler = new MM_GUIHandler();
    }

    public void DBLogin(string username,string password)
    {
        ClientDBEventHandler clientDBEventHandler = new ClientDBEventHandler();
        ILogger logger = new UnityLogger();
        PersistentData data = UnityConfig.GetPersistentDataContainer().persistentData;
        dbClient = new DBClient(config,clientDBEventHandler,data,logger);
        Message_ClientRequest_Login msg = new Message_ClientRequest_Login()
        {
            name = username,
            password = password
        };
        clientDBEventHandler.SendMessageOnConnectionSuccesfull(mM_GUIHandler, dbClient, msg);
        dbClient.Connect();
    }

    public void DBRegister(string username, string password)
    {
        ClientDBEventHandler clientDBEventHandler = new ClientDBEventHandler();
        ILogger logger = new UnityLogger();
        PersistentData data = UnityConfig.GetPersistentDataContainer().persistentData;
        dbClient = new DBClient(config, clientDBEventHandler, data, logger);
        Message_ClientRequest_Register msg = new Message_ClientRequest_Register()
        {
            name = username,
            password = password
        };
        clientDBEventHandler.SendMessageOnConnectionSuccesfull(mM_GUIHandler,dbClient, msg);

        dbClient.Connect();
    }

    // Use this for initialization
    public void StartMatchMakingClient () {
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
        if (dbClient != null)
            dbClient.Update(Time.deltaTime);
        mM_GUIHandler.Update();
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
