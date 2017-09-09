using System;
using UnityEngine;

/// <summary>
/// Handler for Message_Response_GameInfo send from server to client whenever a new client connects or disconnects
/// </summary>
public class Client_MessageHandler_GameInfo
{
    public Client_MessageHandler_GameInfo()
    {
    }

    public void Handle(Message_Response_GameInfo data)
    {
        Debug.Log("Data in regards to game info recieved " + data.players.Count + " data.players.Count");
    }
}