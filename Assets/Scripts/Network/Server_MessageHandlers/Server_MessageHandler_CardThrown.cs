using System;
using UnityEngine;

/// <summary>
/// The handler responsible for the object Message_Request_ThrowCard
/// </summary>
public class Server_MessageHandler_CardThrown
{
    public Server_MessageHandler_CardThrown()
    {
    }

    public void Handle(Message_Request_ThrowCard data)
    {
        Debug.Log("data.cardName " + data.cardName);
    }
}