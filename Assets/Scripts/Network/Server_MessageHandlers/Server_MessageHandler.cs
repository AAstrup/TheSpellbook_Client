using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that is responsible for getting a serialized object 
/// and giving it to the correct handler
/// </summary>
public class Server_MessageHandler {

    Server_MessageHandler_JoinGame handler_JoinGame;
    Server_MessageHandler_CardThrown handler_CardThrown;
    public Server_MessageHandler(Server server)
    {
        handler_JoinGame = new Server_MessageHandler_JoinGame(server);
        handler_CardThrown = new Server_MessageHandler_CardThrown();
    }

    /// <summary>
    /// The method responsible for getting a serialized object 
    /// and giving it to the correct handler
    /// </summary>
    /// <param name="data"></param>
    /// <param name="client"></param>
    public void Handle(object data, Server_ServerClient client)
    {
        if (data is Message_Request_JoinGame)
            handler_JoinGame.Handle((Message_Request_JoinGame)data, client);
        else if (data is Message_Request_ThrowCard)
            handler_CardThrown.Handle((Message_Request_ThrowCard)data);
        else
            throw new Exception("Data type UKNOWN! Type: " + data.ToString());          
    }
}