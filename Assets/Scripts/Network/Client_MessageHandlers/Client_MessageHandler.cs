using System;
using System.Runtime.InteropServices;

/// <summary>
/// Responsible for handling responses from the sever as objects and
/// finding a suitable handler for the object
/// </summary>
public class Client_MessageHandler
{
    private Client_MessageHandler_GameInfo handler_GameInfo;
    private Client_MessageHandler_InQueue handler_InQueue;
    private Client_MessageHandler_MatchFound handler_MatchFound;

    public Client_MessageHandler(Client client)
    {
        handler_GameInfo = new Client_MessageHandler_GameInfo();
        handler_InQueue = new Client_MessageHandler_InQueue();
        handler_MatchFound = new Client_MessageHandler_MatchFound(client);
    }

    public void Handle(object data)
    {
        if (data is Message_Response_InQueue)
            handler_InQueue.Handle((Message_Response_InQueue)data);
        else if (data is Message_Response_GameInfo)
            handler_GameInfo.Handle((Message_Response_GameInfo)data);
        else if (data is Message_Updates_MatchFound)
            handler_MatchFound.Handle((Message_Updates_MatchFound)data);
        else
            throw new Exception("Data type UKNOWN! Type: " + data.ToString());
    }
}