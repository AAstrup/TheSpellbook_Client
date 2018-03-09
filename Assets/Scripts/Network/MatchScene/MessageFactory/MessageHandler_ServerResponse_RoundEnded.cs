using SharedClientServerGameObjectMessages;
using System;

internal class MessageHandler_ServerResponse_RoundEnded : IMessageHandlerCommandClient
{
    public Type GetMessageTypeSupported()
    {
        return typeof(Message_ServerResponse_RoundEnded);
    }

    public void Handle(object objData)
    {

    }
}