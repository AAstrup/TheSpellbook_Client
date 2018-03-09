using ClientServerSharedGameObjectMessages;
using SharedClientServerGameObjectMessages;
using System;

internal class MessageHandler_ServerCommand_StartNewRound : IMessageHandlerCommandClient
{
    public Type GetMessageTypeSupported()
    {
        return typeof(Message_ServerCommand_RoundEnd);
    }

    public void Handle(object objData)
    {
        var data = (Message_ServerCommand_RoundEnd)objData;
        InGameWrapper.instance.resetLogic.QueueNewRound(data.timeNextRoundStart, "Next round in ");
    }
}