using SharedClientServerGameObjectMessages;
using System;

internal class MessageHandler_ServerCommand_PlayerLeft : IMessageHandlerCommandClient
{
    public Type GetMessageTypeSupported()
    {
        return typeof(Message_ServerCommand_PlayerLeft);
    }

    public void Handle(object objData)
    {
        var data = (Message_ServerCommand_PlayerLeft)objData;
        InGameWrapper.instance.playersWrapper.PlayerLeft(data.playerLeftGUID);
    }
}