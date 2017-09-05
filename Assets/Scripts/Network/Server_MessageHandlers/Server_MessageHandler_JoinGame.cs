using System;
using UnityEngine;

/// <summary>
/// The handler responsible for the object Message_Request_JoinGame
/// This is send from the client when they are connected
/// </summary>
public class Server_MessageHandler_JoinGame
{
    private int playerGUID;
    private Server server;

    public Server_MessageHandler_JoinGame(Server server)
    {
        this.server = server;
    }

    /// <summary>
    /// Responsible for adding a GUID to the users data
    /// Then a update is send to all players to update their information about other players
    /// and give the newly arrived player the existing players data
    /// </summary>
    /// <param name="data"></param>
    /// <param name="client"></param>
    public void Handle(Message_Request_JoinGame data, Server_ServerClient client)
    {
        Debug.Log("Recieved JoinGame msg");
        Shared_PlayerInfo playerInfo = data.playerInfo;
        var uniqueId = GetUniquePlayerNr();
        playerInfo.GUID = uniqueId;
        server.gameInfo.AddPlayer(playerInfo);
        server.clientManager.UpdatePlayerInfo(data.playerInfo, client);

        for (int i = 0; i < server.clientManager.GetClients().Count; i++)
        {
            var msg = new Message_Response_GameInfo(server.gameInfo.GetPlayers());
            server.messageSender.Send(msg, server.clientManager.GetClients()[i]);
        }
    }

    /// <summary>
    /// Returns a new player GUID in form of a player number
    /// </summary>
    /// <returns>The GUID or player number</returns>
    private int GetUniquePlayerNr()
    {
        return playerGUID++;
    }
}