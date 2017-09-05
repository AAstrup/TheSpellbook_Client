﻿using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

/// <summary>
/// The class responsible for recieving and handling messages from the server
/// </summary>
public class Server_MessageReciever
{
    private Server_Connection connection;
    private Server_MessageHandler messageHandler;

    public Server_MessageReciever(Server_Connection connection,Server_MessageHandler messageHandler)
    {
        this.connection = connection;
        this.messageHandler = messageHandler;
    }

    /// <summary>
    /// Check for player request messages and process them
    /// </summary>
    public void CheckForPlayerRequestMessages()
    {
        foreach (Server_ServerClient serverClient in connection.GetClientManager().GetClients())
        {
            NetworkStream networkStream = serverClient.tcp.GetStream();
            if (networkStream.DataAvailable)
            {
                BinaryFormatter form = new BinaryFormatter();
                var data = form.Deserialize(networkStream);

                if (data != null)
                {
                    OnIncomingData(serverClient, data);
                }
            }
        }
    }

    /// <summary>
    /// Method called when recieving data
    /// </summary>
    /// <param name="client">Client issuing the call</param>
    /// <param name="data">Object of data send</param>
    private void OnIncomingData(Server_ServerClient client, object data)
    {
        messageHandler.Handle(data, client);
    }
}