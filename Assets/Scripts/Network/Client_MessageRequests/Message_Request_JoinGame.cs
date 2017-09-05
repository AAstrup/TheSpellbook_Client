using System;

/// <summary>
/// A request from client to the server for joining the game
/// This is send after the tcp connection is established!
/// </summary>
[Serializable]
public class Message_Request_JoinGame
{
    public Shared_PlayerInfo playerInfo;
    public Message_Request_JoinGame(Shared_PlayerInfo playerInfo)
    {
        this.playerInfo = playerInfo;
    }
}