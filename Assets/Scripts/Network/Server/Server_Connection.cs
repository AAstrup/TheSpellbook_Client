using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

/// <summary>
/// This class sets the server up for listening and accepting 
/// incoming clients
/// </summary>
public class Server_Connection
{
    private TcpListener listner;
    private Server_ClientManager clientManager;
    private int threads;

    /// <summary>
    /// Creates a reciever
    /// On creation it will start listening
    /// </summary>
    /// <param name="clientManager">The manager of the clients connected</param>
    public Server_Connection(Server_ClientManager clientManager)
    {
        this.clientManager = clientManager;

        StartsListening();
    }

    /// <summary>
    /// Starts Listening
    /// </summary>
    private void StartsListening()
    {
        try
        {
            listner = new TcpListener(IPAddress.Any, AppConfig.Port);
            listner.Start();

            StartNewListner();
        }
        catch (Exception e) { Debug.Log(e.Message); }
    }

    public Server_ClientManager GetClientManager()
    {
        return clientManager;
    }

    /// <summary>
    /// Starts new async listner
    /// </summary>
    private void StartNewListner()
    {
        threads++;
        listner.BeginAcceptTcpClient(AccepTcpClient, listner);
    }

    /// <summary>
    /// Accept client connecting
    /// Will start a new listner
    /// </summary>
    /// <param name="ar"></param>
    private void AccepTcpClient(IAsyncResult ar)
    {
        TcpListener newListener = (TcpListener)ar.AsyncState;

        Server_ServerClient sc = new Server_ServerClient(newListener.EndAcceptTcpClient(ar));
        clientManager.AddClient(sc);

        StartNewListner();

        Debug.Log("Somebody has connected!");
    }
}