using System;

/// <summary>
/// A message request from client to server to throw a card
/// </summary>
[Serializable]
public class Message_Request_ThrowCard
{
    public string cardName;
}