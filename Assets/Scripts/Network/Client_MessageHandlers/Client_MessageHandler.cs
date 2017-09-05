using System;

/// <summary>
/// Responsible for handling responses from the sever as objects and
/// finding a suitable handler for the object
/// </summary>
public class Client_MessageHandler
{
    private Client_MessageHandler_GameInfo handler_GameInfo;

    public Client_MessageHandler()
    {
        handler_GameInfo = new Client_MessageHandler_GameInfo();
    }
    public void Handle(object data)
    {
        if (data is Message_Response_GameInfo)
            handler_GameInfo.Handle((Message_Response_GameInfo)data);
        else
            throw new Exception("Data type UKNOWN! Type: " + data.ToString());
    }
}