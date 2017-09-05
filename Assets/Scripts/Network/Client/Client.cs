using System;
using System.IO;
using System.Net.Sockets;
using UnityEngine;

/// <summary>
/// The class that starts everything up for the client
/// Everything is started in its constructor
/// </summary>
public class Client
{
    private Shared_PlayerInfo myPlayerInfo;
    ClientConnection connection;
    Client_MessageSender sender;
    Client_MessageHandler messageHandler;
    Client_MessageReciever reciever;

    public Client(Shared_PlayerInfo myPlayerInfo)
    {
        this.myPlayerInfo = myPlayerInfo;
        connection = new ClientConnection();
        sender = new Client_MessageSender(connection);
        messageHandler = new Client_MessageHandler();
        reciever = new Client_MessageReciever(connection, messageHandler);

        sender.RegisterAtServer(myPlayerInfo);
    }

    /// <summary>
    /// Checks for responses from the server
    /// </summary>
    public void Update()
    {
        reciever.CheckForServerResponseMessages();
    }
}