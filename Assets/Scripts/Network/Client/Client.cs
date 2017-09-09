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
    ClientConnection connection;
    Client_MessageSender sender;
    IMessageHandler messageHandler;
    Client_MessageReciever reciever;

    public Client(ConnectionInfo connectionInfo, IMessageHandler messageHandler)
    {
        connection = new ClientConnection(connectionInfo);
        sender = new Client_MessageSender(connection);
        this.messageHandler = messageHandler;
        reciever = new Client_MessageReciever(connection, messageHandler);

        sender.RegisterAtServer(AppConfig.GetPersistentData().PlayerInfo);
    }

    /// <summary>
    /// Checks for responses from the server
    /// </summary>
    public void Update()
    {
        reciever.CheckForServerResponseMessages();
    }

    internal void KillConnection()
    {
        connection.GetSocket().Close();
    }
}