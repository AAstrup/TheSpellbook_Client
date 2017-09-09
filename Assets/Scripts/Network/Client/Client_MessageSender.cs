using System;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

/// <summary>
/// Responsible for sending messages to the server
/// </summary>
public class Client_MessageSender
{
    private ClientConnection connection;

    public Client_MessageSender(ClientConnection connection)
    {
        this.connection = connection;
    }

    /// <summary>
    /// Sends a serializable object
    /// </summary>
    /// <param name="serializable">The serializable object being send</param>
    public void Send(object serializable)
    {
        if (!connection.SocketIsReady())
        {
            Debug.Log("Socket not ready, message not set!");
            return;
        }

        BinaryFormatter form = new BinaryFormatter();
        form.Serialize(connection.GetSocket().GetStream(), serializable);
    }

    /// <summary>
    /// Register the client at the server with the player information
    /// </summary>
    /// <param name="playerInfo"></param>
    public void RegisterAtServer(Shared_PlayerInfo playerInfo)
    {
        var msg = new Message_Request_JoinQueue(playerInfo);
        Send(msg);
    }
}