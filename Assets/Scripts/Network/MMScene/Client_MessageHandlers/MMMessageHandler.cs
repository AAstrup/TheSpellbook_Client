using System;
using System.Runtime.InteropServices;
using UnityEngine;

/// <summary>
/// Responsible for handling responses from the sever as objects and
/// finding a suitable handler for the object
/// </summary>
public class MMMessageHandler : IMessageHandler
{
    private Client_MessageHandler_InQueue handler_InQueue;
    private Client_MessageHandler_MatchFound handler_MatchFound;

    public MMMessageHandler()
    {
        handler_InQueue = new Client_MessageHandler_InQueue();
    }

    public void Init(Client client)
    {
        handler_MatchFound = new Client_MessageHandler_MatchFound(client);
    }

    public void Handle(object data)
    {
        Debug.Log("MMMessageHandler got data of type " + data.GetType());
        if (data is Message_Response_InQueue)
            handler_InQueue.Handle((Message_Response_InQueue)data);
        else if (data is Message_Updates_MatchFound)
            handler_MatchFound.Handle((Message_Updates_MatchFound)data);
        else
            throw new Exception("Data type UKNOWN! Type: " + data.ToString());
    }
}