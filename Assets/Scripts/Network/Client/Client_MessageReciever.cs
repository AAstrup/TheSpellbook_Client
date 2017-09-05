using System;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

/// <summary>
/// Responsible for recieving messages from the server
/// </summary>
public class Client_MessageReciever
{
    private ClientConnection connection;
    private Client_MessageHandler messageHandler;

    public Client_MessageReciever(ClientConnection connection, Client_MessageHandler messageHandler)
    {
        this.messageHandler = messageHandler;
        this.connection = connection;
    }

    /// <summary>
    /// Checks for server messages
    /// </summary>
    public void CheckForServerResponseMessages()
    {
        if (!connection.SocketIsReady())
            return;
        if (!connection.GetStream().DataAvailable)
            return;

        BinaryFormatter form = new BinaryFormatter();
        var data = form.Deserialize(connection.GetStream());

        if (data != null)
        {
            messageHandler.Handle(data);
        }
    }
}